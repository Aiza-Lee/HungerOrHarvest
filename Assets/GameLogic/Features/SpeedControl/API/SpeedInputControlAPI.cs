using GameLogic.World;

namespace GameLogic.Features.SpeedControl {
	public static class SpeedControlAPI {
		public static void SetSpeedControlInputEnabled(bool enabled) {
			var res = GameWorldMono.MainWorld.GetResource<SpeedControlInputResource>();
			res.EnabledInput = enabled;
		}
	}
}