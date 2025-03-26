namespace GameLogic
{
	public class SetVillWorkCmd : ICommand {
		private VillLogicBase _vill;
		private ArchLogicBase _arch;

		public string CmdTitle => "设置村民Work";
		public string Description => $"建筑ID:{_arch.ID}  建筑类型:{_arch.ArchType}  村民ID:{_vill.ID}  新状态:Work";
		public string FailReason { get; private set; }

		public bool Check() {
			if (_vill == null) {
				FailReason = "村民不存在";
				return false;
			}
			if (_arch == null) {
				FailReason = "建筑不存在";
				return false;
			}
			if (_arch.CheckCapacity()) {
				return true;
			}
			FailReason = "建筑已满";
			return false;
		}

		public void Execute() {
			_vill.SetWork(_arch.ID);
			_arch.AddVill(_vill);
		}

		public ICommand Init(ICmdData data) {
			var d = data as SetVillWorkCmdData;
			_vill = d.Vill;
			_arch = d.Arch;
			return this;
		}
	}

	public class SetVillWorkCmdData : ICmdData {
		public VillLogicBase Vill;
		public ArchLogicBase Arch;
	}
}