namespace GameLogic
{
	public class UnlockOLCmd : ICommand {
		private OL _ol;

		public bool Check() { return true; }

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