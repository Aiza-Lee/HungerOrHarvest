using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Common.View {
	/// <summary>
	/// 负责将实体的 TransformComponent 实时同步到 Unity 的 Transform。
	/// </summary>
	public class TransformSyncSystem : ISystem {
		public int Priority => 20000;
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
			var query = _world.CreateQueryBuilder()
							.WithAll<TransformComponent>()
							.Build();
			query.ForEach(e => {
				var transComp = e.GetComponent<TransformComponent>();
				if (!transComp.Dirty) return;
				transComp.ClearDirty();
				var go = EntityMono.GetByEntityId(e.ID);
				transComp.ApplyToTransform(go.transform);
			});
		}
	}
}