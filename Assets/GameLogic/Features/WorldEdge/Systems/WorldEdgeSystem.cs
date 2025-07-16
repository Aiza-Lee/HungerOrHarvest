using GameLogic.Common.Logic;
using GameLogic.Features.Generator;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.WorldEdge {
	/// <summary>
	/// WorldEdgeSystem 负责维护WorldEdgeResource，记录村庄的边界范围。
	/// </summary>
	public class WorldEdgeSystem : ISystem {
		public int Priority => 700;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var query = _world.CreateQueryBuilder().WithAll<ArchGeneratedEventComp_Logic>().Build();
			query.ForEach(e => {
				var archEvent = e.GetComponent<ArchGeneratedEventComp_Logic>();
				var gid = archEvent.ArchGid;
				var entity = GameWorldMono.GidToEntity[gid];
				var olComp = entity.GetComponent<OLComponent>();

				// 更新边界范围
				_world.GetResource<WorldEdgeResource>().UpdateArchEdge(olComp.OL);
			});
			
			_world.CreateQueryBuilder().WithAll<LayerGeneratedEventComp_Logic>().Build().ForEach(e => {
				var layerEvent = e.GetComponent<LayerGeneratedEventComp_Logic>();
				var gid = layerEvent.LayerGid;
				var lyr = GameWorldMono.GidToEntity[gid].GetComponent<OLComponent>().OL.LYR;
				_world.GetResource<WorldEdgeResource>().UpdateLayerRange(lyr);
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}