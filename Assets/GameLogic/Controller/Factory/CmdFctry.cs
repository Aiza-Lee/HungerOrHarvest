using NSFrame;

namespace GameLogic
{
	public static class CmdFctry {

		#region GameOpt
		public static ICommand CreateArch(ArchType type, OL ol) {
			var data = new CreateArchCmdData() { ArchType = type, OL = ol };
			return new CreateArchCmd().Init(data);
		}
		public static ICommand CreateVill(VillType type, OL ol) {
			var data = new CreateVillCmdData() { VillType = type, OL = ol };
			return new CreateVillCmd().Init(data);
		}
		public static ICommand SetSpeed(float speed) {
			var data = new SetSpeedCmdData() { Speed = speed };
			return new SetSpeedCmd().Init(data);
		}
		public static ICommand SetVillSpare(VillLogicBase vill) {
			var data = new SetVillSpareCmdData() { Vill = vill };
			return new SetVillSpareCmd().Init(data);
		}
		public static ICommand SetVillSpare(ulong vID) {
			return SetVillSpare(WorldMgr.Inst.FindVill(vID));
		}
		public static ICommand SetVillWork(VillLogicBase vill, ArchLogicBase arch) {
			var data = new SetVillWorkCmdData() { Vill = vill, Arch = arch };
			return new SetVillWorkCmd().Init(data);
		}
		public static ICommand SetVillWork(ulong vID, ulong aID) {
			return SetVillWork(WorldMgr.Inst.FindVill(vID), WorldMgr.Inst.FindArch(aID));
		}
		public static ICommand TogglePause() {
			return new TogglePauseCmd();
		}
		public static ICommand UnlockOL(OL ol) {
			var data = new UnlockOLCmdData() { OL = ol };
			return new UnlockOLCmd().Init(data);
		}
		#endregion

		#region SaveOpt
		public static ICommand LoadSave(SaveInfo saveInfo) {
			var data = new LoadSaveCmdData() { saveInfo = saveInfo };
			return new LoadSaveCmd().Init(data);
		}
		public static ICommand NewWrold() {
			return new NewWorldCmd().Init(null);
		}
		public static ICommand SaveGame() {
			return new SaveGameCmd().Init(null);
		}
		#endregion

	}
}