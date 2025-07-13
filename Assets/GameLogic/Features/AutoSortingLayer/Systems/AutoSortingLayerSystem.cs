using GameLogic.Common.Logic;
using GameLogic.Features.Elements;
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

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var entities = _world.CreateQueryBuilder()
							.WithAny<VillIdentityComponent, LayerIdentityComponent, ArchIdentityComponent>()
							.Build();
			entities.ForEach(entity => {
				if (entity.HasComponent<VillIdentityComponent>()) {
					var coordComp = entity.GetComponent<CoordComponent>();
					if (!coordComp.IsDirty) return;
					AutoSortingLayerAPI.SetSortingLayerByCoordY(entity, coordComp.Coord.Y);
				} else {
					var OlComp = entity.GetComponent<OLComponent>();
					if (!OlComp.IsDirty) return;
					AutoSortingLayerAPI.SetSortingLayerByOlLyr(entity, OlComp.OL.LYR);
				}
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}