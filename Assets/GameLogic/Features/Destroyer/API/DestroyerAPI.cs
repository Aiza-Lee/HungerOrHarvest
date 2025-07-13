using GameLogic.World;

namespace GameLogic.Features.Destroyer {
	public static class DestroyerAPI {
		public static void DestroyArch(ulong gid) {
			GameWorldMono.MainWorld.GetResource<ArchDestroyResource>()
				.ArchToDestroyGid.Add(gid);
		}

		public static void DestroyVill(ulong gid) {
			GameWorldMono.MainWorld.GetResource<VillDestroyResource>()
				.VillToDestroy.Add(gid);
		}
	}
}