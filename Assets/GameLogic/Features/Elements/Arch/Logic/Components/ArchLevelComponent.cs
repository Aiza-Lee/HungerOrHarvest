using NsEcsFrame.Core;

namespace GameLogic.Features.Arch {
	public class ArchLevelComponent : IComponent {
		public int Level = 0;
		public bool IsDirty = true;
	}
}