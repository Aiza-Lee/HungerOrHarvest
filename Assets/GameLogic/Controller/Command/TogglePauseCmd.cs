namespace GameLogic
{
	public class TogglePauseCmd : ICommand {
		public bool Check() => true;

		public void Execute() {
			TickTrigger.Inst.Pause = !TickTrigger.Inst.Pause;
		}

		public ICommand Init(ICmdData _) => this;
	}
}