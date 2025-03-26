namespace GameLogic
{
	public class UnlockOLCmd : ICommand {
		private OL _ol;

		public string CmdTitle => "解锁OL";
		public string Description => $"{_ol}";
		public string FailReason => $"{_ol} 已解锁";

		public bool Check() {
			if (WorldMgr.Inst.IsOLUnlocked(_ol)) { return false; }
			return true;
		}

		public ICommand Init(ICmdData data) {
			var d = (UnlockOLCmdData)data;
			_ol = d.OL;
			return this;
		}
		public void Execute() {
			WorldMgr.Inst.UnlockOL(_ol);
		}
	}

	public class UnlockOLCmdData : ICmdData {
		public OL OL;
	}
}