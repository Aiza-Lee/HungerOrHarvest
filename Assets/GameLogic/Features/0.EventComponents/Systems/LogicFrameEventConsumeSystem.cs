using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	/// <summary>
	/// LogicFrameEventConsumeSystem 负责消耗事件
	/// </summary>
	public class LogicFrameEventConsumeSystem : ISystem {
		public int Priority => 2000;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _levelUpQuery;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_levelUpQuery = _world.CreateQueryBuilder().WithAll<VillJobLevelUpEventComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_levelUpQuery.Build().ForEach(entity => entity.RemoveComponent<VillJobLevelUpEventComponent>());
		}
		public void OnRenderUpdate(float _) { }
	}
}