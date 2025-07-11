using GameLogic.Common.Logic;
using GameLogic.World;

namespace GameLogic.Features.ClearWorld {
	/// <summary>
	/// 提供清空当前世界的命令。
	/// </summary>
	public static class ClearWorldAPI {
		public static void Clear() {

			WorldClearRegistry.Inst.RespondWorldClear();

			var world = GameWorldMono.MainWorld;

			var systems = world.SystemManager.GetAllSystems();
			foreach (var system in systems) {
				if (system is IWorldClearRespondable respondable) {
					respondable.RespondWorldClear();
				}
			}

			var reses = world.GetAllResources();
			foreach (var res in reses) {
				if (res is IWorldClearRespondable respondable) {
					respondable.RespondWorldClear();
				}
			}

			var entities = world.GetAllEntities();
			foreach (var entity in entities) {
				if (!entity.HasComponent<IgnoreWorldClearComponent>()) {
					if (entity.HasComponent<GidComponent>()) {
						var gid = entity.GetComponent<GidComponent>().Gid;
						if (GameWorldMono.GidToEntity.ContainsKey(gid)) {
							GameWorldMono.GidToEntity.Remove(gid);
						}
					}
					world.DestroyEntity(entity.ID);
				}
			}
		}
	}
}