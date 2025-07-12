using System.Collections.Generic;
using GameLogic.Common.View;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	[System.Serializable]
	public class CameraConfigResource : IResource {

		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;
		public ChangeInfo DefaultCameraSizeChangeInfo;
		public ChangeInfo DefaultCameraStopPositionChangeInfo;
		public ChangeInfo DefaultForwardPositionChangeInfo;
		public ChangeInfo DefaultBackwardPositionChangeInfo;
	}
}