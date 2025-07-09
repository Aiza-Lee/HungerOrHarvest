using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// ArchDestroyerSystem 负责销毁建筑实体。
	/// </summary>
	public class ArchDestroyerSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var res = _world.GetResource<ArchDestroyResource>();
			var toDestroy = res.ArchToDestroy;
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