using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// 记录摄像机输入的资源类
	/// </summary>
	public class CameraInputSystem : ISystem {
		private IWorld _world;
		public int Priority => 10000;
		public bool Enabled { get; set; }

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }

		public void OnRenderUpdate(float _) {
			var cameraInput = _world.GetResource<CameraInputResource>();
			if (!cameraInput.EnableCameraInput) {
				return;
			}

			cameraInput.MoveLeftKey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
			cameraInput.MoveRightKey = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
			cameraInput.MoveLeftKeyUp = Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
			cameraInput.MoveRightKeyUp = Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);

			cameraInput.MoveForwardKeyDown = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
			cameraInput.MoveBackwardKeyDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

			cameraInput.IsSizeDirty = true;
			if (Input.GetKeyDown(KeyCode.F1)) {
				cameraInput.TargetCameraSizeIndex = 0;
			} else if (Input.GetKeyDown(KeyCode.F2)) {
				cameraInput.TargetCameraSizeIndex = 1;
			} else if (Input.GetKeyDown(KeyCode.F3)) {
				cameraInput.TargetCameraSizeIndex = 2;
			} else if (Input.GetKeyDown(KeyCode.F4)) {
				cameraInput.TargetCameraSizeIndex = 3;
			} else {
				cameraInput.IsSizeDirty = false;
			}
		}
	}
}