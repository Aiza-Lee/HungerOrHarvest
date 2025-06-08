using System;
using System.Collections.Generic;
using GameLogic.Model.Element.Arch;
using GameLogic.Utilities;

namespace GameLogic.Model.Element.Vill {
	public abstract class VillLogicBase : ISaveable<VillSaveBase>, IVillLogic {
		public abstract VillType VillType { get; }

		private LogicImpler _logicImpler;

		public event Action<Coord> OnCoordChange {
			add => _logicImpler.OnCoordChange += value;
			remove => _logicImpler.OnCoordChange -= value;
		}
		public event Action<JobType> OnJobLevelUp {
			add => _expHelper.OnJobLevelUp += value;
			remove => _expHelper.OnJobLevelUp -= value;
		}
		public event Action<float> OnVitChanged {
			add => _vitHelper.OnVitChanged += value;
			remove => _vitHelper.OnVitChanged -= value;
		}

		private ExpHelper _expHelper => _logicImpler.ExpHelper;
		private RepoBuffHelper _repoBuffHelper => _logicImpler.RepoBuffHelper;
		private IVitHelper _vitHelper => _logicImpler.VitHelper;
		private BondArchHelper _bondArchHelper => _logicImpler.BondArchHelper;

		#region IVillLogic
		public ulong ID => _logicImpler.ID;
		public string FirstName => _logicImpler.FirstName;
		public string LastName => _logicImpler.LastName;
		public Coord Coord => _logicImpler.Coord;
		public ulong HomeID => _bondArchHelper.HomeID;
		public ulong BondedWorkArchID => _bondArchHelper.BondedWorkArchID;
		public bool IsHomeless => _bondArchHelper.IsHomeless;
		public bool IsWorkless => _bondArchHelper.IsWorkless;
		public string CurStateDescription => _logicImpler.StateMachine.CurStateDescription;

		public void LogicDestroy() => _logicImpler.LogicDestroy();

		public void BondArch(ArchLogicBase arch) => _bondArchHelper.BondArch(arch);
		public void DisBondWorkArch() => _bondArchHelper.DisBondWorkArch();
		public void DisBondHome() => _bondArchHelper.DisBondHome();
		public void Move(Coord dltCoord) => _logicImpler.Move(dltCoord);
		public List<JobType> GetSortedJobLevels() => _expHelper.GetSortedJobLevels();
		public float GetJobExpProportion(JobType jobType) => _expHelper.GetJobExpProportion(jobType);
		public int GetJobLevel(JobType jobType) => _expHelper.GetJobLevel(jobType);
		public float GetVitPercentage() => _vitHelper.CurVitProportion;
		#endregion

		#region ISaveable
		protected abstract VillSaveBase GetDerivedSave();
		public VillSaveBase GetSave() {
			var save = GetDerivedSave();
				save.LogicImpler	= _logicImpler.GetSave();
			return save;
		}

		protected abstract void DerivedInitFromSave(VillSaveBase save);
		public virtual void InitFromSave(VillSaveBase save) {
			DerivedInitFromSave(save);
			_logicImpler = new(this);
			_logicImpler.InitFromSave(save.LogicImpler);
		}
		#endregion
	}
}