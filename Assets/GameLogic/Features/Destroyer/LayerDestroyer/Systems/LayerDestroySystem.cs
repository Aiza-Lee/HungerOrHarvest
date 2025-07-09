using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// LayerDestroyerSystem 负责销毁Layer实体。
	/// </summary>
	public class LayerDestroyerSystem : ISystem {
		public int Priority => 500;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var res = _world.GetResource<LayerDestroyResource>();
			var toDestroy = res.LayerToDestroy;
			foreach (var entityId in toDestroy) {
				var go = EntityMono.GetByEntityId(entityId);
				if (go != null) {
					GameObject.Destroy(go);
				}
				_world.DestroyEntity(entityId);
			}
			toDestroy.Clear();
		}
		public void OnRenderUpdate(float _) { }
	} 
}