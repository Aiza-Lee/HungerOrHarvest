using System.Collections.Generic;
using GameLogic.Common.Utils;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public static class ArchAPI {
		public static List<EntityId> GetBondedVills(Entity entity) {
			var bondedVills = new List<EntityId>();
			var bondComp = entity.GetComponent<BondToVillComponent>();
			foreach (var villId in bondComp.BondedVillGids) {
				bondedVills.Add(villId.GetEntity().ID);
			}
			return bondedVills;
		}

		public static int ArchLevel(Entity entity) {
			var archComp = entity.GetComponent<ArchLevelComponent>();
			return archComp.Level;
		}

		public static bool TryBondVill(Entity arch, Entity vill) {
			var bondComp = arch.GetComponent<BondToVillComponent>();
			if (bondComp.BondedVillGids.Contains(vill.GetGid())) {
				return false;
			}
			var archType = arch.GetComponent<ArchIdentityComponent>().ArchType;
			var config = GameWorldMono.MainWorld.GetResource<ArchConfigResource>().GetConfig(archType);
			var lConfig = config.LevelConfigs[ArchLevel(arch)];
			if (bondComp.BondedVillGids.Count >= lConfig.MaxVillContain) {
				return false;
			}
			bondComp.BondedVillGids.Add(vill.GetGid());
			return true;
		}
	}
}