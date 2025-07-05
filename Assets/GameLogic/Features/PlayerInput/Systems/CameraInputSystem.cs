using GameLogic.Resources.MainCamera;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.PlayerInput {
	public class CameraInputSystem : ISystem {
		private IWorld _world;
		public int Priority => -1;
		public bool Enabled { get; set; }

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() {}
		public void OnDestroy() {}
		public void OnLogicUpdate(float _) {}

		public void OnRenderUpdate(float deltaTime) {
			var cameraInput = _world.GetResource<CameraInputResource>();
			cameraInput.CameraMoveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
			cameraInput.CameraMoveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
			cameraInput.CameraMoveForward = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
			cameraInput.CameraMoveBackward = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

			cameraInput.CameraSizeTo1 = Input.GetKeyDown(KeyCode.F1);
			cameraInput.CameraSizeTo2 = Input.GetKeyDown(KeyCode.F2);
			cameraInput.CameraSizeTo3 = Input.GetKeyDown(KeyCode.F3);
			cameraInput.CameraSizeTo4 = Input.GetKeyDown(KeyCode.F4);
		}
	}
}