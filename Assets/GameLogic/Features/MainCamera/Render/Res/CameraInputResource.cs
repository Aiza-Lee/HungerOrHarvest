using System;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	[Serializable]
	public class CameraInputResource : IResource {
		public bool EnableCameraInput;

		public bool CameraMoveLeft;
		public bool CameraMoveRight;
		public bool CameraMoveForward;
		public bool CameraMoveBackward;

		public int TargetCameraSizeIndex;
	}
}