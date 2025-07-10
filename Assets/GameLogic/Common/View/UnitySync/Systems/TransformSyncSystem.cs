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
			// Debug.Log($"{query.Count} entities with TransformComponent found in TransformSyncSystem");
			query.ForEach(e => {
				var transComp = e.GetComponent<TransformComponent>();
				// Debug.Log($"TransformSyncSystem: Processing entity {e.ID} with TransformComponent");
				if (!transComp.Dirty) return;
				var go = EntityMono.GetByEntityId(e.ID);
				transComp.ApplyToTransform(go.transform);
				transComp.ClearDirty();
			});
		}
	}
}