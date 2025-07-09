using GameLogic.World;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// VillDestroyerSystem 负责销毁村民实体
	/// </summary>
	public class VillDestroyerSystem : ISystem {
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
			var res = _world.GetResource<VillDestroyResource>();
			var toDestroy = res.VillToDestroy;
			foreach (var gid in toDestroy) {
				var entityId = GameWorldMono.GidToEntity[gid].ID;
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