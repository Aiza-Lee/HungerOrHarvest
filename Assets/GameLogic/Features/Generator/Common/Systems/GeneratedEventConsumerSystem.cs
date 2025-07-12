using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// GeneratedEventConsumerSystem_Logic 负责回收生成事件的实体。
	/// </summary>
	public class GeneratedEventConsumerSystem_Logic : ISystem {
		public int Priority => 2000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var query = _world.CreateQueryBuilder()
				.WithAny<ArchGeneratedEventComp_Logic, LayerGeneratedEventComp_Logic, VillGeneratedEventComp_Logic>()
				.Build();
			query.ForEach(e => _world.DestroyEntity(e.ID));
		}
		public void OnRenderUpdate(float _) { }
	}

	/// <summary>
	/// GeneratedEventConsumerSystem_View 负责回收生成事件的实体。
	/// </summary>
	public class GeneratedEventConsumerSystem_View : ISystem {
		public int Priority => 22000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var query = _world.CreateQueryBuilder()
				.WithAny<ArchGeneratedEventComp_View, ArchGeneratedEventComp_View, ArchGeneratedEventComp_View>()
				.Build();
			query.ForEach(e => _world.DestroyEntity(e.ID));

		}
	}
}