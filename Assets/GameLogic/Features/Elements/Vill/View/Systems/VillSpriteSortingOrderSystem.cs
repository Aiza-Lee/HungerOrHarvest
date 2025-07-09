using GameLogic.Common.View;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// VillSpriteSoringOrderSystem 负责在smoothedCoord变化时更新精灵的sortingOrder(依赖于Coord的dirty状态)。
	/// </summary>
	public class VillSpriteSoringOrderSystem : ISystem {
		public int Priority => 2000;
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
			var entities = _world.CreateQueryBuilder()
								.WithAll<SpriteRendererComponent>()
								.Build();
			entities.ForEach(entity => {
				var srComp = entity.GetComponent<SpriteRendererComponent>();
				if (!srComp.IsDirty) return;
				if (!EntityMono.GetByEntityId(entity.ID).TryGetComponent<SpriteRenderer>(out var sr)) return;
				srComp.ApplyToSpriteRenderer(sr);
				srComp.ClearDirty();
			});
		}
	} 
}