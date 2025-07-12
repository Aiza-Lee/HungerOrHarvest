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
	}
}