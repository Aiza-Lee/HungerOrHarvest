using GameLogic.Features.TickCounter;
using GameLogic.World;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	public static class SaveGameAPI {
		/// <summary>
		/// 保存当前世界的状态。
		/// </summary>
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