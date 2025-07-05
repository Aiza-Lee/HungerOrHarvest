
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {

	[System.Serializable]
	public class MainCameraResource : IResource {
		public float Size;
		public float MoveSpeed;
		public EntityId? FocusEntityId;
	}
}