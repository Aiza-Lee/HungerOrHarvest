using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Layer {
	[System.Serializable]
	public class LayerIdentityComponent : IComponent {
		public LayerType LayerType;
	}
}