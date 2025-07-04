using NsEcsFrame.Core;

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
			
		}
	}
}