using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// DestroyedEventComsumerSystem_Logic
	/// </summary>
	public class DestroyedEventConsumerSystem_Logic : ISystem {
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
				.WithAny<VillDestroyedEventComp_Logic, ArchDestroyedEventComp_Logic>()
				.Build();
			query.ForEach(e => _world.DestroyEntity(e.ID));
		}
		public void OnRenderUpdate(float _) { }
	} 

	/// <summary>
	/// DestroyedEventComsumerSystem_View
	/// </summary>
	public class DestroyedEventConsumerSystem_View : ISystem {
		public int Priority => 22000;
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
				.WithAny<VillDestroyedEventComp_View, ArchDestroyedEventComp_View>()
				.Build();
			query.ForEach(e => _world.DestroyEntity(e.ID));
		}
		public void OnRenderUpdate(float _) { }
	}
}