using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// 方向系统负责处理角色的朝向视觉维护
	/// </summary>
	public class VillDirectionSystem : ISystem {
		public int Priority => 19500;
		public bool Enabled { get; set; }

		private EntityQueryBuilder _queryBuilder;

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_queryBuilder = _world.CreateQueryBuilder().WithAll<VillIdentityComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			_queryBuilder.Build().ForEach(entity => {
				var coord = entity.GetComponent<CoordComponent>().Coord;
				var routecomp = entity.GetComponent<RoutePlanComponent>();
				var direction = new Coord(0, 0);
				try {
					direction = routecomp.MoveRoute[routecomp.CurMoveIndex] - coord;
				} catch { }
				VillViewAPI.SetDirection(entity, direction);
			});
		}
	} 
}