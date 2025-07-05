using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Resources.Configs {
	public class CameraConfig : IResource {

		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;

		public void CopyFrom(IResource other) {
			if (other is CameraConfig otherConfig) {
				CAMERA_MOVE_SPEED = otherConfig.CAMERA_MOVE_SPEED;
				CAMERA_STOP_LENGTH = otherConfig.CAMERA_STOP_LENGTH;
				CameraSizes = new List<float>(otherConfig.CameraSizes);
			} else {
				throw new System.InvalidCastException("Cannot copy from non-CameraConfig resource.");
			}
		}
	}
}