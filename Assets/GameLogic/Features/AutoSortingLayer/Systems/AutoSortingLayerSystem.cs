using GameLogic.Common.Logic;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Elements.Decorations;
using GameLogic.Features.Layer;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.AutoSortingLayer {
	/// <summary>
	/// AutoSortingLayerSystem 负责自动排序层
	/// </summary>
	public class AutoSortingLayerSystem : ISystem {
		public int Priority => 900;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _query;


		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_query = _world.CreateQueryBuilder()
							.WithAny<VillIdentityComponent, LayerIdentityComponent, ArchIdentityComponent>()
							.WithAny<DecorationIdentityComp>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_query.Build().ForEach(entity => {
				if (entity.HasComponent<CoordComponent>()) {
					var coordComp = entity.GetComponent<CoordComponent>();
					if (!coordComp.IsDirty) return;
					AutoSortingLayerAPI.Set_SortingOrderAndLayer_ByCoordY(entity, coordComp.Coord.Y);
				} else if (entity.HasComponent<OLComponent>()) {
					var OlComp = entity.GetComponent<OLComponent>();
					if (!OlComp.IsDirty) return;
					AutoSortingLayerAPI.Set_SortingLayer_ByOlLyr(entity, OlComp.OL.LYR);
				}
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}