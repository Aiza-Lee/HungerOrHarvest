using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	public class CameraConfigResource : IResource {

		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;
	}
}