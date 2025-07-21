using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public class ArchConfigComponent : IComponent, IIgnoreSaveComponent {
		public ArchConfigBase LogicConfig;
		public ArchArtConfigBase ArtConfig;
	}
}