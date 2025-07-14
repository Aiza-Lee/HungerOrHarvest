using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Utils;
using GameLogic.Features.Events;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public static class ArchQueryAPI {
		private static readonly EntityQueryBuilder _archQueryBuilder = GameWorldMono.MainWorld.CreateQueryBuilder()
			.WithAll<ArchIdentityComponent>();

		public static List<Entity> GetAllArchs() {
			var archs = new List<Entity>();
			_archQueryBuilder.Build().ForEach(arch => archs.Add(arch));
			return archs;
		}
		public static List<Entity> GetAllArchs(ArchType archType) {
			var archs = new List<Entity>();
			_archQueryBuilder.Build().ForEach(arch => {
				if (arch.GetComponent<ArchIdentityComponent>().ArchType == archType) { archs.Add(arch); }
			});
			return archs;
		}

		public static int GetLevel(Entity arch) => arch.GetComponent<ArchLevelComponent>().Level;
		public static ArchType GetType(Entity arch) => arch.GetComponent<ArchIdentityComponent>().ArchType;

		public static IEnumerable<ulong> GetBondedVills(Entity arch) => arch.GetComponent<BondToVillComponent>().BondedVillGids;
		public static int GetBondedVillCount(Entity arch) => arch.GetComponent<BondToVillComponent>().BondedVillGids.Count;
		public static int GetArchMaxContain(Entity arch) => GetLevelConfig(arch).MaxVillContain;
		public static bool CanBondAnotherVill(Entity arch) {
			var lConfig = GetLevelConfig(arch);
			var bondedCnt = arch.GetComponent<BondToVillComponent>().BondedVillGids.Count;
			return bondedCnt < lConfig.MaxVillContain;
		}
		public static bool HasBondedVill(Entity arch, ulong villGid) {
			return arch.GetComponent<BondToVillComponent>().BondedVillGids.Contains(villGid);
		}

		public static ArchArtConfigBase GetArtConfig(ArchType archType) {
			return GameWorldMono.MainWorld.GetResource<ArchConfigResource>().GetArtConfig(archType);
		}
		public static ArchLevelConfigBase GetLevelConfig(Entity arch) {
			var level = GetLevel(arch);
			var archType = GetType(arch);
			return GameWorldMono.MainWorld.GetResource<ArchConfigResource>()
				.GetConfig(archType).LevelConfigs[level];
		}

		public static Entity GetBondableWorkArch(ArchType archType) {
			Entity resArch = null;
			_archQueryBuilder.Build().ForEach(arch => {
				var archComp = arch.GetComponent<ArchIdentityComponent>();
				if (archComp.ArchType != archType) return;
				if (arch.GetComponent<BondToVillComponent>().BondedVillGids.Count < GetLevelConfig(arch).MaxVillContain) {
					resArch = arch;
					return;
				}
			});
			return resArch;
		}

		public static bool GetIsSpaceEnoughForVill(int villCount, ArchType archType) {
			var cnt = 0;
			_archQueryBuilder.Build().ForEach(arch => {
				if (arch.GetComponent<ArchIdentityComponent>().ArchType != archType) return;
				cnt += GetLevelConfig(arch).MaxVillContain - arch.GetComponent<BondToVillComponent>().BondedVillGids.Count;
				if (cnt >= villCount) return;
			});
			return cnt >= villCount;
		}

		public static bool IsAnyArch(ArchType archType) {
			return _archQueryBuilder.Build().Any(arch => arch.GetComponent<ArchIdentityComponent>().ArchType == archType);
		}
	}

	public static class ArchDirectOperationAPI { }

	public static class ArchRequestAPI {
		public static void RequestBondToVill(Entity arch, Entity vill) {
			arch.AddComponent(new BondToVillRequestComponent() { VillGid = vill.GetGid(), });
		}
		public static void RequestDisbondVill(Entity arch, Entity vill) {
			arch.AddComponent(new DisbondVillRequestComponent() { VillGid = vill.GetGid(), });
		}
	}
}