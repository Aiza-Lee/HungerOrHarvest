namespace GameLogic
{
	public class CmdFctry {
		private CmdFctry() {}
		public static CmdFctry Inst { get; private set; } = new();

		public ICommand CreateArchCMD(ArchType type, OL ol) {
			var data = new CreateArchCmdData() { ArchType = type, OL = ol };
			return new CreateArchCmd().Init(data);
		}

		public ICommand CreateVillCMD(VillType type, OL ol) {
			var data = new CreateVillCmdData() { VillType = type, OL = ol };
			return new CreateVillCmd().Init(data);
		}
		
		public ICommand SetVillStaCMD(ulong villID, StaType sta) {
			var data = new SetVillStaCmdData() { VillID = villID, Sta = sta };
			return new SetVillStaCmd().Init(data);
		}

		public ICommand UnlockOLCMD(OL ol) {
			var data = new UnlockOLCmdData() { OL = ol };
			return new UnlockOLCmd().Init(data);
		}

		public ICommand TogglePauseCMD() {
			return new TogglePauseCmd();
		}

		public ICommand SetSpeedCMD(float speed) {
			var data = new SetSpeedCmdData() { Speed = speed };
			return new SetSpeedCmd().Init(data);
		}

	}
}