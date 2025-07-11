using GameLogic.World;

namespace GameLogic.Features.TickSpeed {
	public static class TickSpeedAPI {
		public static void SetTickSpeed(float speed) {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			tsRes.TickSpeed = speed;
			tsRes.IsDirty = true;
		}
		public static void PauseTick(bool pause) {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			if (tsRes.IsPaused != pause) {
				tsRes.IsPaused = pause;
				tsRes.IsDirty = true;
			}
		}
		public static void TogglePauseTick() {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			tsRes.IsPaused = !tsRes.IsPaused;
			tsRes.IsDirty = true;
		}
	}
}