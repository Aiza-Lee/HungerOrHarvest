using NsEcsFrame.Core;

namespace GameLogic.Common.Logic {
	/// <summary>
	/// OLToCoordSystem 负责将 OLComponent 转换为 CoordComponent。
	/// </summary>
	public class OLToCoordSystem : ISystem {
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
				.WithAll<OLComponent, CoordComponent>()
				.Build();
			entities.ForEach(entity => {
				var olComp = entity.GetComponent<OLComponent>();
				if (!olComp.IsDirty) return;
				var coordComp = entity.GetComponent<CoordComponent>();
				coordComp.Coord = olComp.OL.ToCoord();
				coordComp.IsDirty = true;
				olComp.IsDirty = false;
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}