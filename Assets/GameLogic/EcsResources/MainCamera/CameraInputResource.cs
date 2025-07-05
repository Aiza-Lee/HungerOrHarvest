using NsEcsFrame.Core;

namespace GameLogic.Resources.MainCamera {
	public class CameraInputResource : IResource {
		public bool CameraMoveLeft;
		public bool CameraMoveRight;
		public bool CameraMoveForward;
		public bool CameraMoveBackward;

		public bool CameraSizeTo1;
		public bool CameraSizeTo2;
		public bool CameraSizeTo3;
		public bool CameraSizeTo4;

		public int? FocusEntityId;

		public void CopyFrom(IResource other) {
			if (other is CameraInputResource otherResource) {
				CameraMoveLeft = otherResource.CameraMoveLeft;
				CameraMoveRight = otherResource.CameraMoveRight;
				CameraMoveForward = otherResource.CameraMoveForward;
				CameraMoveBackward = otherResource.CameraMoveBackward;

				CameraSizeTo1 = otherResource.CameraSizeTo1;
				CameraSizeTo2 = otherResource.CameraSizeTo2;
				CameraSizeTo3 = otherResource.CameraSizeTo3;
				CameraSizeTo4 = otherResource.CameraSizeTo4;

				FocusEntityId = otherResource.FocusEntityId;
			} else {
				throw new System.InvalidCastException("Cannot copy from a resource of different type.");
			}
		}
	}
}