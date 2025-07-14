using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	public class BondToVillRequestComponent : IComponent {
		public ulong VillGid;
	}
	public class DisbondVillRequestComponent : IComponent {
		public ulong VillGid;
	}
	public class VillEnterWorkArchRequestComponent : IComponent { }
	public class VillEnterHomeArchRequestComponent : IComponent { }
	public class VillLeaveArchRequestComponent : IComponent { }
}