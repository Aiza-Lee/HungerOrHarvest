using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// 记录摄像机输入的资源类
	/// </summary>
	public class CameraInputSystem : ISystem {
		private IWorld _world;
		public int Priority => 0;
		public bool Enabled { get; set; }

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }

		public void OnRenderUpdate(float deltaTime) {
			var cameraInput = _world.GetResource<CameraInputResource>();
			cameraInput.CameraMoveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
			cameraInput.CameraMoveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
			cameraInput.CameraMoveForward = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
			cameraInput.CameraMoveBackward = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

			if (Input.GetKeyDown(KeyCode.F1)) {
				cameraInput.TargetCameraSizeIndex = 0;
			} else if (Input.GetKeyDown(KeyCode.F2)) {
				cameraInput.TargetCameraSizeIndex = 1;
			} else if (Input.GetKeyDown(KeyCode.F3)) {
				cameraInput.TargetCameraSizeIndex = 2;
			} else if (Input.GetKeyDown(KeyCode.F4)) {
				cameraInput.TargetCameraSizeIndex = 3;
			} else {
				cameraInput.TargetCameraSizeIndex = -1;
			}
		}
	}
}