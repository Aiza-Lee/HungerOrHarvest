using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Utils;
using GameLogic.Features.Events;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public static class ArchQueryAPI {
		public static int GetLevel(Entity arch) => arch.GetComponent<ArchLevelComponent>().Level;
		public static ArchType GetType(Entity arch) => arch.GetComponent<ArchIdentityComponent>().ArchType;

		public static IEnumerable<ulong> GetBondedVills(Entity arch) {
			return arch.GetComponent<BondToVillComponent>().BondedVillGids;
		}
		public static int GetBondedVillCount(Entity arch) {
			return arch.GetComponent<BondToVillComponent>().BondedVillGids.Count;
		}
		public static int GetArchMaxContain(Entity arch) {
			return GetArchLevelConfig(arch).MaxVillContain;
		}
		public static bool CanBondAnotherVill(Entity arch) {
			var lConfig = GetArchLevelConfig(arch);
			var bondedCnt = arch.GetComponent<BondToVillComponent>().BondedVillGids.Count;
			return bondedCnt < lConfig.MaxVillContain;
		}
		public static bool HasBondedVill(Entity arch, ulong villGid) {
			return arch.GetComponent<BondToVillComponent>().BondedVillGids.Contains(villGid);
		}


		public static ArchLevelConfigBase GetArchLevelConfig(Entity arch) {
			var level = GetLevel(arch);
			var archType = GetType(arch);
			return GameWorldMono.MainWorld.GetResource<ArchConfigResource>()
				.GetConfig(archType).LevelConfigs[level];
		}
	}

	public static class ArchDirectOperationAPI { }

	public static class ArchRequestAPI {
		public static void RequestBondToVill(Entity arch, Entity vill) {
			arch.AddComponent(new BondToVillRequestComponent() { VillGid = vill.GetGid(), });
		}
	}
}