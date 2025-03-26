namespace GameLogic
{
	public class TogglePauseCmd : ICommand {
		public string CmdTitle => "切换暂停状态";
		public string Description => $"当前状态:{(TickTrigger.Inst.Pause ? "暂停" : "运行")}";
		public string FailReason => string.Empty;

		public bool Check() => true;

		public void Execute() {
			TickTrigger.Inst.Pause = !TickTrigger.Inst.Pause;
		}

		public ICommand Init(ICmdData _) => this;
	}
}