using GameLogic.Features.TickCounter;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// NewDayRefreshSystem 负责每天开始的时候刷新村民的状态
	/// </summary>
	public class NewDayRefreshSystem : ISystem {
		public int Priority => 700;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _query;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_query = world.CreateQueryBuilder().WithAll<VillIdentityComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var cnter = _world.GetResource<TickCounterResource>();
			if (!cnter.IsDayFirstTick) return;

			_query.Build().ForEach(vill => {
				var vit = vill.GetComponent<VillVitalityComponent>();
				vit.RecoverChances = vill.GetComponent<VillConfigComponent>().LogicConfig.VitConfig.RecoverChancePerDay;
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}