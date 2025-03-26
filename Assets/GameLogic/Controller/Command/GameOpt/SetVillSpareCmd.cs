namespace GameLogic
{
	public class SetVillSpareCmd : ICommand {

		private VillLogicBase _vill;

		public string CmdTitle => "设置村民Spare";
		public string Description => $"村民ID:{_vill.ID}  新状态:Spare";
		public string FailReason => "村民不存在";

		public bool Check() {
			return _vill != null;
		}

		public void Execute() {
			_vill.SetSpare();
		}

		public ICommand Init(ICmdData data) {
			var d = data as SetVillSpareCmdData;
			_vill = d.Vill;
			return this;
		}
	}
	public class SetVillSpareCmdData : ICmdData {
		public VillLogicBase Vill;
	}
}