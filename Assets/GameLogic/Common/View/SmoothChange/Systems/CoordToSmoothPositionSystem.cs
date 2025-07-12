using GameLogic.Common.Logic;
using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	/// <summary>
	/// CoordToSmoothPositionSystem 负责处理CoordComponent 到 SmoothChangeStat 的转换逻辑。
	/// 仅限同时含有 CoordComponent 和 SmoothPositionStatComponent 的实体。
	/// </summary>
	public class CoordToSmoothPositionSystem : ISystem {
		public int Priority => 1000;
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
				.WithAll<CoordComponent, SmoothPositionStatComponent>().Build();
			entities.ForEach(entity => {
				var coordComp = entity.GetComponent<CoordComponent>();
				if (!coordComp.IsDirty) return;
				coordComp.IsDirty = false;
				var statComp = entity.GetComponent<SmoothPositionStatComponent>();
				statComp.StartAChange(entity, coordComp.Coord.ToVec3DefaultY());
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}