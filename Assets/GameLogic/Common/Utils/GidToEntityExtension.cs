using GameLogic.Common.Logic;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Common.Utils {
	public static class GidToEntityExtension {
		public static Entity GetEntity(this ulong gid) {
			return GameWorldMono.GidToEntity[gid];
		}
		public static ulong GetGid(this Entity entity) {
			return entity.GetComponent<GidComponent>().Gid;
		}
		public static ulong GetGid(this EntityId entityId) {
			return GameWorldMono.MainWorld.GetEntity(entityId).GetGid();
		}
	}
}