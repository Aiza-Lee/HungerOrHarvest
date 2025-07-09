using NsEcsFrame.Core;

namespace GameLogic.Features.TickCounter {
	/// <summary>
	/// 负责记录Tick，在一切逻辑类之前触发
	/// <para> Tick计数逻辑：表示当前世界正在经历第几个Tick，每天从1开始 </para>
	/// </summary>
	public class TickCounterSystem : ISystem {
		public int Priority => -1;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var config = _world.GetResource<TickConfigResource>().TickConfig;
			var cnterRes = _world.GetResource<TickCounterResource>();
			cnterRes.TodayTickCount++;
			cnterRes.TickCount++;

			if (cnterRes.TodayTickCount == config.DAY_TICKS + config.NIGHT_TICKS + 1) {
				cnterRes.TodayTickCount = 1;
				cnterRes.DayCount++;
			}
			
			cnterRes.IsDay = cnterRes.TodayTickCount <= config.DAY_TICKS;
			cnterRes.DayProcess = (float) cnterRes.TodayTickCount / config.DAY_TICKS;
			cnterRes.NightProcess = (float) (cnterRes.TodayTickCount - config.DAY_TICKS) / config.NIGHT_TICKS;

			cnterRes.IsDayFirstTick = cnterRes.TodayTickCount == 1;
			cnterRes.IsNightFirstTick = cnterRes.TodayTickCount == config.DAY_TICKS + 1;
			cnterRes.IsDayLastTick = cnterRes.TodayTickCount == config.DAY_TICKS;
			cnterRes.IsNightLastTick = cnterRes.TodayTickCount == config.DAY_TICKS + config.NIGHT_TICKS;
		}
		public void OnRenderUpdate(float _) { }
	}
}