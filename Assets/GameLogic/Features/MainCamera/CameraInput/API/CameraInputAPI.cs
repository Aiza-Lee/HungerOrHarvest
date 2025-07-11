using GameLogic.World;

namespace GameLogic.Features.MainCamera {
	public static class CameraInputAPI {
		public static void SetCameraInputEnabled(bool enabled) {
			var res = GameWorldMono.MainWorld.GetResource<CameraInputResource>();
			res.EnableCameraInput = enabled;
		}
	}
}