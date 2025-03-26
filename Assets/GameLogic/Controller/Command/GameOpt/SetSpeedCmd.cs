namespace GameLogic
{
	public class SetSpeedCmd : ICommand {
		private float _speed;

		public string CmdTitle => "设置时间流逝速度";
		public string Description => $"设置为:{_speed}";
		public string FailReason => string.Empty;

		public bool Check() => true;

		public void Execute() {
			TickTrigger.Inst.Speed = _speed;
		}

		public ICommand Init(ICmdData data) {
			_speed = ((SetSpeedCmdData)data).Speed;
			return this;
		}
	
	}
	public class SetSpeedCmdData : ICmdData {
		public float Speed;
	}
}