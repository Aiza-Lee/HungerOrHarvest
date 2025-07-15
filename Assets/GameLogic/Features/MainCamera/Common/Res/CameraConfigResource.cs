using System.Collections.Generic;
using GameLogic.Common.View;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	[System.Serializable]
	public class CameraConfigResource : IResource {

		public float CameraSizeFocus => CameraSizes[0];
		public float CameraSizeNormal => CameraSizes[1];
		public float CameraSizeFar => CameraSizes[2];
		public float CameraSizeMax => CameraSizes[^1];


		[Header("Camera Control Settings")]
		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;
		public ChangeInfo SizeChangeInfo;
		public ChangeInfo StopPositionChangeInfo;
		public ChangeInfo ForwardPositionChangeInfo;
		public ChangeInfo BackwardPositionChangeInfo;

		[Header("Camera Follow Settings")]
		public bool EnableControlCameraFollow = true;
		public ChangeInfo FollowChangeInfo;
		public ChangeInfo UnfollowChangeInfo;
	}
}