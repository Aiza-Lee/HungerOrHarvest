using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	public class ArchDestroyedEventComp_Logic : IComponent {
		public ulong ArchGid;
	}
	public class ArchDestroyedEventComp_View : IComponent {
		public ulong ArchGid;
	}
}
