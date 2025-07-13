using GameLogic.Features.TickSpeed;
using GameLogic.World;
using NSFrame;

namespace GameLogic.Features.WorldDataManager {
	public static class WorldDataManagerAPI {
		/// <summary>
		/// 设置saveInfo对象
		/// </summary>
		public static void SetSaveInfo(SaveInfo saveInfo) {
			var res = GameWorldMono.MainWorld.GetResource<SaveInfoResource>();
			res.SaveInfo = saveInfo;
		}

		/// <summary>
		/// 加载存档数据
		/// </summary>
		public static void LoadData(SaveInfo saveInfo) {
			SetSaveInfo(saveInfo);
			var res = GameWorldMono.MainWorld.GetResource<LoadGameCmdResource>();
			res.LoadGameCommand = true;
		}

		/// <summary>
		/// 保存当前世界数据到存档。
		/// </summary>
		public static void Save(bool isAutoSave) => SaveDataUtils.Save(isAutoSave);

		/// <summary>
		/// 清除当前世界数据。
		/// </summary>
		public static void ClearWorld() => ClearWorldUtils.ClearWorld();

		public static bool IsWorldLoaded() {
			return GameWorldMono.MainWorld.GetResource<SaveInfoResource>().IsLoaded;
		}

		/// <summary>
		/// 创建一个新的随机世界，需要提供世界名称。
		/// </summary>
		/// <param name="worldName"></param>
		public static void NewRandomWorld(string worldName) {
			var world = GameWorldMono.MainWorld;
			var res = world.GetResource<NewWorldInfoResource>();
			var baseInfo = world.GetResource<RandomWorldConfigResource>().GetRandomWorldBaseInfo();
			res.NewWorldInfo = new() {
				WorldName = worldName,
				BaseInfo = baseInfo
			};
			TickSpeedAPI.PauseTick(false);
			world.GetResource<SaveInfoResource>().IsLoaded = true;
		}
	}
}