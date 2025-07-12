using GameLogic.Common.Logic;
using GameLogic.Features.TickCounter;
using GameLogic.World;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	public static class SaveLoadDataAPI {
		public static void SetSaveInfo(SaveInfo saveInfo) {
			var res = GameWorldMono.MainWorld.GetResource<SaveInfoResource>();
			res.SaveInfo = saveInfo;
		}
		public static void LoadData(SaveInfo saveInfo) {
			SetSaveInfo(saveInfo);
			var res = GameWorldMono.MainWorld.GetResource<LoadGameCmdResource>();
			res.LoadGameCommand = true;
		}
		public static SaveInfo Save(bool isAutoSave) {
			var world = GameWorldMono.MainWorld;
			var saveInfo = SaveSystem.CreateSaveFile(world.Name);
			world.GetResource<SaveInfoResource>().SaveInfo = saveInfo;

			var extendSaveInfo = new ExtendSaveInfo(isAutoSave, world.GetResource<TickCounterResource>().DayCount);
			var gameSaveData = new GameSaveData(world);

			saveInfo.SaveObject(extendSaveInfo);
			saveInfo.SaveObject(gameSaveData);

			return saveInfo;
		}

		public static void ClearWorld() {

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

			world.GetResource<SaveInfoResource>().LoadedSave = false;
		}

		public static bool IsWorldLoaded() {
			var res = GameWorldMono.MainWorld.GetResource<SaveInfoResource>();
			return res.LoadedSave;
		}
	}
}