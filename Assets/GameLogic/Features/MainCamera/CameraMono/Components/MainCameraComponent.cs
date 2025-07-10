
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {

	[System.Serializable]
	public class MainCameraComponent : IComponent {
		public EntityId FocusEntityId = EntityId.NullEntityId;
	}
}