using GameLogic.Common.Logic;
using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	/// <summary>
	/// CoordToSmoothChangeStatSystem 负责处理SmoothedCoordComponent 到 SmoothChangeStat 的转换逻辑。
	/// </summary>
	public class CoordToSmoothChangeStatSystem : ISystem {
		public int Priority => 100;
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
				.WithAll<SmoothedCoordComponent, SmoothChangeStatComponent>().Build();
			entities.ForEach(entity => {
				var coordComp = entity.GetComponent<SmoothedCoordComponent>();
				if (!coordComp.IsDirty) return;
				var statComp = entity.GetComponent<SmoothChangeStatComponent>();
				statComp.AddNewChange(
					true,
					ChangeTargetType.Transform_Position,
					coordComp.ChangeCurveType,
					coordComp.TotalTime,
					coordComp.Coord.ToVec3DefaultY()
				);
				coordComp.IsDirty = false;
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}