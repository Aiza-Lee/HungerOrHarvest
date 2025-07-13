using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Features.Elements;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	public static class VillAPI {
		private static IWorld World => GameWorldMono.MainWorld;

		public static List<ulong> GetAllVillGids() {
			var query = World.CreateQueryBuilder()
				.WithAll<VillIdentityComponent>()
				.Build();
			var gids = new List<ulong>();
			query.ForEach(e => {
				gids.Add(e.GetComponent<GidComponent>().Gid);
			});
			return gids;
		}
		public static List<EntityId> GetAllVillEntityIds() {
			var query = World.CreateQueryBuilder()
				.WithAll<VillIdentityComponent>()
				.Build();
			var entityIds = new List<EntityId>();
			query.ForEach(e => {
				entityIds.Add(e.ID);
			});
			return entityIds;
		}

		public static float QueryVillVit(ulong gid) => QueryVillVit(GameWorldMono.GidToEntity[gid].ID);
		public static float QueryVillVit(EntityId id) {
			var entity = World.GetEntity(id);
			return entity.GetComponent<VillVitalityComponent>().Vit;
		}

		public static void BondArch(EntityId villId, EntityId archId) {
			var vill = World.GetEntity(villId);
			var arch = World.GetEntity(archId);
			var archType = arch.GetComponent<ArchIdentityComponent>().ArchType;
			if (archType == ArchType.Cottage) {
				vill.GetComponent<BondToArchComponent>().HomeArchGid = arch.GetComponent<GidComponent>().Gid;
			} else {
				vill.GetComponent<BondToArchComponent>().WorkArchGid = arch.GetComponent<GidComponent>().Gid;
			}		
		}

	}
}