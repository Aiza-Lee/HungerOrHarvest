using System;
using NSFrame;

namespace GameLogic
{
	public abstract class VillLogicBase : ISaveable<VillSaveBase> {

		public abstract VillType VillType { get; }

		private ulong _id;
		private string _firstName, _lastName;
		private Coord _coord;
		private JTList<int> _jobLevel_F;
		private JTList<float> _jobExps_F;
		private RTList<float> _consBuffs, _prodBuffs;
		private StaMachine _staMachine;

		public ulong ID => _id;
		public string FirstName => _firstName;
		public string LastName => _lastName;
		public Coord Coord => _coord;
		public RTList<float> ConsBuffs => _consBuffs;
		public RTList<float> ProdBuffs => _prodBuffs;
		public StaType CurSta {
			get { return _staMachine.CurSta; }
			set { _staMachine.SetStaByType(value); }
		}

		public event Action<Coord> OnCoordChange;


		#region Public Method
		public void SetStaMachine(StaMachine sm) => _staMachine = sm;
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
		#endregion

		#region ISaveable
		protected abstract VillSaveBase GetDerivedSave();
		public VillSaveBase GetSave() {
			var save = GetDerivedSave();
				save.ID = _id;
				save.FirstName = _firstName;
				save.LastName = _lastName;
				save.Coord = _coord;
				save.JobLevel = _jobLevel_F.Clone();
				save.JobExps = _jobExps_F.Clone();
				save.ConsBuffs = _consBuffs.Clone();
				save.ProdBuffs = _prodBuffs.Clone();
				save.StaMachine = _staMachine.GetSave();
			return save;
		}

		protected abstract void DerivedInitFromSave(VillSaveBase save);
		public virtual void InitFromSave(VillSaveBase save) {
			DerivedInitFromSave(save);
			_id = save.ID;
			_firstName = save.FirstName;
			_lastName = save.LastName;
			_coord = save.Coord;
			_jobLevel_F = save.JobLevel.ConvertToFull();
			_jobExps_F = save.JobExps.ConvertToFull();
			_consBuffs = save.ConsBuffs;
			_prodBuffs = save.ProdBuffs;
			_staMachine = LogicFctry.Inst.LoadStaMachine(save.StaMachine);
			_staMachine.SetOwner(this);
		}
		#endregion
	}
}