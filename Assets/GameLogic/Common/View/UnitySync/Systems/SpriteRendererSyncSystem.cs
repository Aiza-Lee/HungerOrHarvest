using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Common.View {
	public class SpriteRendererSyncSystem : ISystem {
		public int Priority => 1000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float deltaTime) {
			var query = _world.CreateQueryBuilder()
				.WithAll<SpriteRendererComponent>()
				.Build();
			query.ForEach(e => {
				var spriteRendererComp = e.GetComponent<SpriteRendererComponent>();
				if (!spriteRendererComp.IsDirty) return;
				var go = EntityMono.GetByEntityId(e.ID);
				spriteRendererComp.ApplyToSpriteRenderer(go.GetComponent<UnityEngine.SpriteRenderer>());
				spriteRendererComp.ClearDirty();
			});
		}
	}
}