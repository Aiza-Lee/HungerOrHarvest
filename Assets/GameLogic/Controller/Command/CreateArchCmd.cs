namespace GameLogic 
{
	public class CreateArchCmd : ICommand {
		private ArchType _archType;
		private OL _ol;

		public ICommand Init(ICmdData data) {
			var d = (CreateArchCmdData)data;
			_archType = d.ArchType;
			_ol = d.OL;
			return this;
		}

		public bool Check() {
			var config = ConstMgr.Inst.Config.FindConfig(_archType);
			if (!RepoMgr.Inst.CheckRequest(config.ConstructCost)) { return false; }
			return true;
		}
		public void Execute() {
			LogicFctry.Inst.NewArch(_archType, _ol);
		}
	}

	public class CreateArchCmdData : ICmdData {
		public ArchType ArchType;
		public OL OL;
	}
}