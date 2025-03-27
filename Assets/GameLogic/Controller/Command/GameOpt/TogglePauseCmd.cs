namespace GameLogic
{
	public class TogglePauseCmd : CommandBase {
		public TogglePauseCmd(string[] args) : base(args) {}

		public override string CmdTitle => "切换暂停状态";
		public override string Description => $"当前状态:{(TickTrigger.Inst.Pause ? "暂停" : "运行")}";
		public override string FailReason => string.Empty;
		public override int ArgCount => 0;

		public override bool Check() => true;

		public override void Execute() {
			TickTrigger.Inst.Pause = !TickTrigger.Inst.Pause;
		}
	}
}