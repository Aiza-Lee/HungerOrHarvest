namespace GameLogic
{
	public class CreateVillCmd : ICommand {

		private VillType _villType;
		private OL _ol;

		public string CmdTitle => "生成村民";
		public string Description => $"类型:{_villType}  位置:{_ol.ToCoord()}";
		public string FailReason => string.Empty;

		public ICommand Init(ICmdData data) {
			var d = (CreateVillCmdData)data;
			_villType = d.VillType;
			_ol = d.OL;
			return this;
		}
		public bool Check() {
			return true;
		}
		public void Execute() {
			LogicFctry.Inst.NewVill(_villType, _ol);
		}
	}

	public class CreateVillCmdData : ICmdData {
		public VillType VillType;
		public OL OL;
	}

}