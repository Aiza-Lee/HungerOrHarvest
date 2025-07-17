using GameLogic.World;

namespace GameLogic.Features.TickCounter {
	public static class TickCounterQueryAPI {
		public static float GetWholeProcess() {
			var res = GameWorldMono.MainWorld.GetResource<TickCounterResource>();
			return res.WholeProcess;
		}
	}
}