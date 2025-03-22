namespace GameLogic
{
	public class SetSpeedCmd : ICommand {
		private float _speed;

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