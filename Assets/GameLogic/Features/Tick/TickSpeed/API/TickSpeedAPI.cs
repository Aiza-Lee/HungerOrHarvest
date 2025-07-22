using GameLogic.World;

namespace GameLogic.Features.TickSpeed {
	public static class TickSpeedAPI {
		public static void SetTickSpeed(float speed) {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			tsRes.TickSpeed = speed;
			tsRes.IsDirty = true;
		}
		public static void SetTickPaused(bool isPaused) {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			if (tsRes.IsPaused != isPaused) {
				tsRes.IsPaused = isPaused;
				tsRes.IsDirty = true;
			}
		}
		public static void TogglePause() {
			var tsRes = GameWorldMono.MainWorld.GetResource<TickSpeedResource>();
			tsRes.IsPaused = !tsRes.IsPaused;
			tsRes.IsDirty = true;
		}
	}
}