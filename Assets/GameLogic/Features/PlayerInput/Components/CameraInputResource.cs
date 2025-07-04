using NsEcsFrame.Core;

namespace GameLogic.Features.PlayerInput {
	public struct CameraInputResource : IResource {
		public bool CameraMoveLeft;
		public bool CameraMoveRight;
		public bool CameraMoveForward;
		public bool CameraMoveBackward;

		public bool CameraSizeTo1;
		public bool CameraSizeTo2;
		public bool CameraSizeTo3;
		public bool CameraSizeTo4;
	}
}