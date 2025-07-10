using GameLogic.Common.Logic;
using NsEcsFrame.Components;
using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	/// <summary>
	/// 负责将Coord直接转换到对应的TransformComponent上。
	/// </summary>
	public class CoordToTransformSystem : ISystem {
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
			var entities = _world.CreateQueryBuilder()
								.WithAll<CoordComponent, TransformComponent>()
								.Build();
			entities.ForEach(entity => {
				var coordComp = entity.GetComponent<CoordComponent>();
				if (!coordComp.IsDirty) return;
				var transComp = entity.GetComponent<TransformComponent>();
				transComp.LocalPosition = coordComp.Coord.ToVec3DefaultY();
				transComp.Dirty = true;
			});
		}
		public void OnRenderUpdate(float _) {}
	} 
}