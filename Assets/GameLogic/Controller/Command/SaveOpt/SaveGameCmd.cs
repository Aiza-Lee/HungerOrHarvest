namespace GameLogic
{
	public class SaveGameCmd : ICommand {
		public string CmdTitle => "保存游戏";
		public string Description => "";
		public string FailReason => "";

		public bool Check() {
			return true;
		}

		public void Execute() {
			SaveMgr.Inst.SaveGame();
		}

		public ICommand Init(ICmdData _) {
			return this;
		}
	}
	public class SaveGameCmdData : ICmdData {}
}