using System;
using GameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public abstract class VillLogicBase : ISaveable<VillSaveBase> {

		public VillLogicBase() {
			EventSystem.AddListener((int)LogicEvt.DayStart_0, OnDayStart);
			EventSystem.AddListener((int)LogicEvt.NightStart_0, OnNightStart);
		}

		#region Injection
		public void SetHomeID(ulong homeID) { 
			_homeID = homeID;
			EventSystem.Invoke<ulong, ulong>((int)LogicEvt.NewRoomDistributed_VuAu_2, _id, _homeID);
		}
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
		private ulong _attachedWorkArchID;


		public ulong ID => _id;
		public string FirstName => _firstName;
		public string LastName => _lastName;
		public Coord Coord => _coord;
		public RTList<float> ConsBuffs_F => _consBuffs.ConvertToFull();
		public RTList<float> ProdBuffs_F => _prodBuffs.ConvertToFull();
		public VillTaskRunner TaskRunner => _taskRunner;
		public ulong AttachedWorkArchID => _attachedWorkArchID;
		public ArchType AttachedWorkArchType => WorldMgr.Inst.FindArch(_attachedWorkArchID).ArchType;

		public bool IsHomeless => _homeID == 0;
		public bool IsWorkless => _attachedWorkArchID == 0;
		public bool IsWorking => _attachedWorkArchID != 0 && _taskRunner.CurTaskType == TaskType.Work;
		public bool IsSleeping => _taskRunner.CurTaskType == TaskType.Sleep;
		public bool IsMoving => _taskRunner.CurTaskType == TaskType.MoveTo;
		public bool IsOnMoveToWork => IsMoving && _attachedWorkArchID != 0;

		private void Destroy() {
			// todo: 
			Debug.Log($"Vill {_id} has no home");
			_taskRunner.Destroy();
			_taskRunner = null;
			EventSystem.RemoveListener((int)LogicEvt.DayStart_0, OnDayStart);
			EventSystem.RemoveListener((int)LogicEvt.NightStart_0, OnNightStart);
			EventSystem.Invoke((int)LogicEvt.VillDestroyed_V_1, this);
		}
		private void OnDayStart() {
			if (!GoWork(_attachedWorkArchID)) {
				_taskRunner.ResetTasks();
			}
		}
		private void OnNightStart() {
			if (!GoSleep()) {
				Destroy();
			}
		}
		private void LevelUp(JobType job) {
			var idx = (int)job;
			_jobLevel_F[idx].Value++;

			var jConfig = ConstMgr.Inst.Config.FindJobConfig(idx);
			var levelNow = _jobLevel_F[idx].Value;
			var nxtLevelConfig = jConfig.JobLevelConfigs[levelNow];

			_consBuffs[idx].Value += nxtLevelConfig.ConsBuff;
			_prodBuffs[idx].Value += nxtLevelConfig.ProdBuff;

			EventSystem.Invoke<ulong, JobType>((int)LogicEvt.VillLevelUp_VuJ_2, _id, job);
		}

		#region Event
		public event Action<Coord> OnCoordChange;
		#endregion

		#region PublicMethods

		public JTList<int> GetSortedJobLevels() {
			JTList<int> res = _jobLevel_F.Clone();
			res.List.Sort((a, b) => b.Value.CompareTo(a.Value));
			res.Full = false;
			return res;
		}

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
		public void Move(Coord dltCoord) {
			_coord += dltCoord;
			OnCoordChange?.Invoke(dltCoord);
		}

		public bool GoWork(ulong archID) {
			if (archID == 0) { return false; }
			var arch = WorldMgr.Inst.FindArch(archID);
			if (arch == null || arch.ArchType == ArchType.Cottage) {
				return false;
			}
			if (!arch.TryBondVill(ID)) {
				return false;
			}
			_taskRunner.ResetTasks(
				LogicFctry.Inst.NewMoveToTask(arch.Coord),
				LogicFctry.Inst.NewWorkTask(archID)
			);
			if (_attachedWorkArchID != archID) {
				var oriArchID = _attachedWorkArchID;
				_attachedWorkArchID = archID;
				EventSystem.Invoke<ulong, ulong, ulong>((int)LogicEvt.VillChengeWork_VuAuAu_3, ID, oriArchID, arch.ID);
			}
			return true;
		}
		public bool GoSleep() {
			if (_taskRunner.CurTaskType == TaskType.Sleep) { return true; }
			if (IsHomeless) {
				return false;
			}
			var cottage = WorldMgr.Inst.FindArch(_homeID);
			if (!cottage.TryBondVill(ID)) {
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
		public void OnBondedArchDestroyed() {
			if (AttachedWorkArchType == ArchType.Cottage) {
				_homeID = 0;
				DelayTrigger.Run(() => RoomMgr.Inst.FindRoomForVill(this), 1);
			} else {
				var oriArchID = _attachedWorkArchID;
				_attachedWorkArchID = 0;
				EventSystem.Invoke<ulong, ulong, ulong>((int)LogicEvt.VillChengeWork_VuAuAu_3, ID, oriArchID, 0);
			}
			_taskRunner.ResetTasks();
		}

		#endregion

		#region ISaveable
		protected abstract VillSaveBase GetDerivedSave();
		public VillSaveBase GetSave() {
			var save = GetDerivedSave();
				save.VillType 			= VillType;
				save.ID 				= _id;
				save.FirstName 			= _firstName;
				save.LastName 			= _lastName;
				save.Coord 				= _coord;
				save.JobLevel 			= _jobLevel_F.Clone();
				save.JobExps 			= _jobExps_F.Clone();
				save.ConsBuffs 			= _consBuffs.Clone();
				save.ProdBuffs 			= _prodBuffs.Clone();
				save.TaskRunner 		= _taskRunner.GetSave();
				save.HomeID 			= _homeID;
				save.AttachedWorkArchID = _attachedWorkArchID;
			return save;
		}

		protected abstract void DerivedInitFromSave(VillSaveBase save);
		public virtual void InitFromSave(VillSaveBase save) {
			DerivedInitFromSave(save);
			_id 				= save.ID;
			_firstName 			= save.FirstName;
			_lastName 			= save.LastName;
			_coord 				= save.Coord;
			_jobLevel_F 		= save.JobLevel.ConvertToFull();
			_jobExps_F 			= save.JobExps.ConvertToFull();
			_consBuffs 			= save.ConsBuffs;
			_prodBuffs 			= save.ProdBuffs;
			_taskRunner 		= LogicFctry.Inst.LoadVillTaskRunner(this, save.TaskRunner);
			_homeID 			= save.HomeID;
			_attachedWorkArchID = save.AttachedWorkArchID;
		}
		#endregion
	}
}