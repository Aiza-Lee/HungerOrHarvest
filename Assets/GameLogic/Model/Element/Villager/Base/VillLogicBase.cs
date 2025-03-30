using System;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public abstract class VillLogicBase : ISaveable<VillSaveBase> {

		public VillLogicBase() {
			EventSystem.AddListener((int)LogicEvt.DayStart, OnDayStart);
			EventSystem.AddListener((int)LogicEvt.NightStart, OnNightStart);
		}

		#region Injection
		public void SetHomeID(ulong homeID) { _homeID = homeID; }
		#endregion

		public abstract VillType VillType { get; }

		private ulong _id;
		private string _firstName, _lastName;
		private Coord _coord;
		private JTList<int> _jobLevel_F;
		private JTList<float> _jobExps_F;
		private RTList<float> _consBuffs, _prodBuffs;
		private VillTaskRunner _taskRunner;
		private ulong _homeID;
		private ulong _prevWorkArchID;

		public ulong ID => _id;
		public string FirstName => _firstName;
		public string LastName => _lastName;
		public Coord Coord => _coord;
		public RTList<float> ConsBuffs_F => _consBuffs.ConvertToFull();
		public RTList<float> ProdBuffs_F => _prodBuffs.ConvertToFull();
		public VillTaskRunner TaskRunner => _taskRunner;
		public ulong HomeID => _homeID;


		private void Destroy() {
			// todo: 
			Debug.Log($"Vill {_id} has no home");
			_taskRunner.Destroy();
			_taskRunner = null;
			EventSystem.RemoveListener((int)LogicEvt.DayStart, OnDayStart);
			EventSystem.RemoveListener((int)LogicEvt.NightStart, OnNightStart);
			EventSystem.Invoke((int)LogicEvt.VillDestroyed_V, this);
		}
		private void OnDayStart() {
			if (!GoWork(_prevWorkArchID)) {
				_taskRunner.ResetTasks();
			}
		}
		private void OnNightStart() {
			if (!GoSleep()) {
				Destroy();
			}
		}

		#region Event
		public event Action<Coord> OnCoordChange;
		#endregion

		#region PublicMethods

		public void AddExp(JTList<float> exps) {
			foreach (var JF in exps.List) {
				var idx = JF.Index;
				_jobExps_F[idx].Value += JF.Value;

				var jConfig = ConstMgr.Inst.Config.FindJobConfig(idx);
				var level = _jobLevel_F[idx].Value;

				if (jConfig.JobLevelConfigs.Count - 1 > level) {
					var demand = jConfig.JobLevelConfigs[level].LevelUpDemand;
					if (_jobExps_F[idx].Value >= demand) {
						_jobExps_F[idx].Value -= demand;
						LevelUp(JF.Job);
					}
				}
			}
		}
		public void LevelUp(JobType job) {
			var idx = (int)job;
			_jobLevel_F[idx].Value++;

			var jConfig = ConstMgr.Inst.Config.FindJobConfig(idx);
			var levelNow = _jobLevel_F[idx].Value;
			var nxtLevelConfig = jConfig.JobLevelConfigs[levelNow];

			_consBuffs[idx].Value += nxtLevelConfig.ConsBuff;
			_prodBuffs[idx].Value += nxtLevelConfig.ProdBuff;
		}
		public void Move(Coord dltCoord) {
			_coord += dltCoord;
			OnCoordChange?.Invoke(dltCoord);
		}

		public bool GoWork(ulong archID) {
			var arch = WorldMgr.Inst.FindArch(archID);
			if (arch == null) {
				return false;
			}
			if (!arch.TryBookPos(ID)) {
				return false;
			}
			_taskRunner.ResetTasks(
				LogicFctry.Inst.NewMoveToTask(arch.Coord),
				LogicFctry.Inst.NewWorkTask(archID)
			);
			_prevWorkArchID = archID;
			return true;
		}
		public bool GoSleep() {
			if (_taskRunner.CurTaskType == TaskType.Sleep) { return true; }
			if (WorldMgr.Inst.FindArch(_homeID) is not CottageLogic cottage) {
				return false;
			}
			if (!cottage.TryBookPos(ID)) {
				return false;
			}
			_taskRunner.ResetTasks(
				LogicFctry.Inst.NewMoveToTask(cottage.Coord),
				LogicFctry.Inst.NewSleepTask(_homeID)
			);
			return true;
		}
		public void GoSpare() {
			_taskRunner.ResetTasks();
		}

		#endregion

		#region ISaveable
		protected abstract VillSaveBase GetDerivedSave();
		public VillSaveBase GetSave() {
			var save = GetDerivedSave();
				save.ID 			= _id;
				save.FirstName 		= _firstName;
				save.LastName 		= _lastName;
				save.Coord 			= _coord;
				save.JobLevel 		= _jobLevel_F.Clone();
				save.JobExps 		= _jobExps_F.Clone();
				save.ConsBuffs 		= _consBuffs.Clone();
				save.ProdBuffs 		= _prodBuffs.Clone();
				save.TaskRunner 	= _taskRunner.GetSave();
				save.HomeID 		= _homeID;
				save.PrevWorkArchID = _prevWorkArchID;
			return save;
		}

		protected abstract void DerivedInitFromSave(VillSaveBase save);
		public virtual void InitFromSave(VillSaveBase save) {
			DerivedInitFromSave(save);
			_id 			= save.ID;
			_firstName 		= save.FirstName;
			_lastName 		= save.LastName;
			_coord 			= save.Coord;
			_jobLevel_F 	= save.JobLevel.ConvertToFull();
			_jobExps_F 		= save.JobExps.ConvertToFull();
			_consBuffs 		= save.ConsBuffs;
			_prodBuffs 		= save.ProdBuffs;
			_taskRunner 	= LogicFctry.Inst.LoadVillTaskRunner(this, save.TaskRunner);
			_homeID 		= save.HomeID;
			_prevWorkArchID = save.PrevWorkArchID;
		}
		#endregion
	}
}