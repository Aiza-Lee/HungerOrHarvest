using GameLogic.Common.Logic;
using GameLogic.World;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Destroyer {
	public static class EntityDestroyUtil {
		public static void DestroyEntity(EntityId entityId) {
			var go = EntityMono.GetByEntityId(entityId);
			if (go != null) {
				GameObject.Destroy(go);
			}
			var entity = GameWorldMono.MainWorld.GetEntity(entityId);
			if (entity != null && entity.HasComponent<GidComponent>()) {
				var gid = entity.GetComponent<GidComponent>().Gid;
				GameWorldMono.GidToEntity.Remove(gid);
			}
			GameWorldMono.MainWorld.DestroyEntity(entityId);
		}
	}
}