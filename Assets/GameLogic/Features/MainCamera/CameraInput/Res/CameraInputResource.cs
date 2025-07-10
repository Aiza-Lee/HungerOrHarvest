using System;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	[Serializable]
	public class CameraInputResource : IResource {
		/// <summary>
		/// 被禁用的时候代表输入的变量仍然可能为true
		/// </summary>
		public bool EnableCameraInput = true;

		public bool MoveLeftKey;
		public bool MoveRightKey;
		public bool MoveForwardKeyDown;
		public bool MoveBackwardKeyDown;

		public bool MoveLeftKeyUp;
		public bool MoveRightKeyUp;

		public int TargetCameraSizeIndex;
		public bool IsSizeDirty;
	}
}