using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	public class VillConfigComponent : IComponent, IIgnoreSaveComponent {
		public VillConfigBase LogicConfig;
		public VillArtConfigBase ArtConfig;
	}
}