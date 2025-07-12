using GameLogic.Common.Logic;
using NsEcsFrame.Components;
using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	/// <summary>
	/// OlToTransformSystem 负责将 OLComponent 转换为 TransformComponent，并清除Dirty标记
	/// </summary>
	public class OlToTransformSystem : ISystem {
		public int Priority => 19000;
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
			var query = _world.CreateQueryBuilder().WithAll<OLComponent, TransformComponent>().Build();
			query.ForEach(e => {
				var olComp = e.GetComponent<OLComponent>();
				if (!olComp.IsDirty) return;
				var transComp = e.GetComponent<TransformComponent>();
				transComp.LocalPosition = olComp.OL.ToVec3DefaultY();
				transComp.Dirty = true;
				olComp.IsDirty = false;
			});
		}
	} 
}