
using NsEcsFrame.Core;

namespace GameLogic.Resources.MainCamera {
	public enum CameraSize {
		Focus,
		Normal,
		Wide,
		WideWide,
	}
	public class MainCameraResource : IResource {
		public CameraSize Size;
		public float MoveSpeed;
		public EntityId? CurFocusEntityId;

		public void CopyFrom(IResource other) {
			if (other is MainCameraResource otherResource) {
				Size = otherResource.Size;
				MoveSpeed = otherResource.MoveSpeed;
				CurFocusEntityId = otherResource.CurFocusEntityId;
			} else {
				throw new System.InvalidCastException("Cannot copy from a resource of different type.");
			}
		}
	}
}