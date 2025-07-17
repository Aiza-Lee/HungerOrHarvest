using GameLogic.Features.TickCounter;
using GameLogic.World;
using NSFrame;

namespace GameLogic.Features.WorldDataManager {
	public static class SaveDataUtils {
		public static void Save(bool isAutoSave) {
			var world = GameWorldMono.MainWorld;
			var saveInfo = SaveSystem.CreateSaveFile(world.Name);
			world.GetResource<CurSaveInfoResource>().SaveInfo = saveInfo;

			var extendSaveInfo = new ExtendSaveInfo(isAutoSave, world.GetResource<TickCounterResource>().DayCount);
			var gameSaveData = new GameSaveData(world);

			saveInfo.SaveObject(extendSaveInfo);
			saveInfo.SaveObject(gameSaveData);

			var evt = world.CreateEntity().AddComponent<SaveEventComponent_Logic>();
		}
	}
}