using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	public class VillDestroyedEventComp_Logic : IComponent {
		public ulong DestroyedVillGid;
	}
	public class VillDestroyedEventComp_View : IComponent {
		public ulong DestroyedVillGid;
	}
}
