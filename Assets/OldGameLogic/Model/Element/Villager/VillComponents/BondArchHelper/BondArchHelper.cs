using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Utilities;
using NSFrame;

namespace OldGameLogic.Model.Element.Vill {
	public class BondArchHelper : ISaveable<BondArchHelperSave>, IBondArchHelper {
		readonly LogicImpler _impler;
		public BondArchHelper(LogicImpler impler) {
			_impler = impler;
		}
		public void LogicDestroy() {
			if (BondedWorkArchID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(BondedWorkArchID));
			if (HomeID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(HomeID));
		}

		// private TaskRunner TaskRnr => _impler.TaskRunner;
		private void OnBondedArchDestroyed(ArchLogicBase arch) {
			DisBondArchImpl(arch);
		}
		private void DisBondArchImpl(ArchLogicBase arch) {
			if (arch == null) { return; }
			arch.OnArchDestroyed -= OnBondedArchDestroyed;
			if (arch is CottageLogic) {
				arch.DisBondVill(_impler.ID);
				arch.VillLeave(_impler.ID);
				HomeID = 0;
				return;
			}

			arch.DisBondVill(_impler.ID);
			BondedWorkArchID = 0;
			EventSystem.Invoke<ulong, ulong, ulong>((int) ModelEvt.VillChengeWork_VuAuAu_3, _impler.ID, arch.ID, 0, NSFrame.EventType.Model);

		}

		#region IVillBondArch
		public ulong HomeID { get; private set; }
		public ulong BondedWorkArchID { get; private set; }
		public bool IsHomeless => HomeID == 0;
		public bool IsWorkless => BondedWorkArchID == 0;

		public void BondArch(ArchLogicBase arch) {
			if (arch is CottageLogic) {
				HomeID = arch.ID;
			} else {
				BondedWorkArchID = arch.ID;
			}
			arch.OnArchDestroyed += OnBondedArchDestroyed;
			arch.BondVill(_impler.ID);
		}
		public void DisBondHome() => DisBondArchImpl(WorldMgr.Inst.FindArch(HomeID));
		public void DisBondWorkArch() => DisBondArchImpl(WorldMgr.Inst.FindArch(BondedWorkArchID));

		#endregion

		#region ISaveable
		public BondArchHelperSave GetSave() {
			return new() {
				HomeID = HomeID,
				BondedWorkArchID = BondedWorkArchID
			};
		}
		public void InitFromSave(BondArchHelperSave save) {
			HomeID = save.HomeID;
			BondedWorkArchID = save.BondedWorkArchID;
		}

		#endregion
	}
}