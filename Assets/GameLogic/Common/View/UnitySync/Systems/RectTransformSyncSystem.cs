using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Common.View {
	public class RectTransformSyncSystem : ISystem {
		public int Priority => 20000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() {}
		public void OnDestroy() {}
		public void OnLogicUpdate(float _) {}
		public void OnRenderUpdate(float deltaTime) {
			var query = _world.CreateQueryBuilder()
				.WithAll<RectTransformComponent>()
				.Build();
			query.ForEach(e => {
				var rectTransComp = e.GetComponent<RectTransformComponent>();
				if (!rectTransComp.IsDirty) return;
				var go = EntityMono.GetByEntityId(e.ID);
				rectTransComp.ApplyToRectTransform(go.GetComponent<RectTransform>());
				rectTransComp.ClearDirty();
			});
		}
	}
}