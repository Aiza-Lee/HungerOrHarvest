using GameLogic.Features.TickCounter;
using NsEcsFrame.Core;

namespace GameLogic.Features.Repo {
	/// <summary>
	/// DayFirstTickClearCounterSystem 负责在每个游戏日开始时清除每日生产和消耗的计数。
	/// </summary>
	public class DayFirstTickClearCounterSystem : ISystem {
		public int Priority => 10;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var tick = _world.GetResource<TickCounterResource>();
			if (tick.IsDayFirstTick) {
				var dailyCnter = _world.GetResource<DailyRepoCounterResource>();
				dailyCnter.DailyProdSum_F.Fill(0f);
				dailyCnter.DailyConsSum_F.Fill(0f);
			}
		}
		public void OnRenderUpdate(float _) { }
	} 
}