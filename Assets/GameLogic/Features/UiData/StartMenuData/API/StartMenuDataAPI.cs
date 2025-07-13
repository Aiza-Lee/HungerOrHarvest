using GameLogic.World;

namespace GameLogic.Features.UiData.StartMenuData {
	public static class StartMenuDataAPI {
		/// <summary>
		/// 是否有任何保存数据发生变化
		/// </summary>
		public static bool IsAnySaveChanged {
			get => GameWorldMono.MainWorld.GetResource<StartMenuDataResource>().IsAnySaveChanged;
			set => GameWorldMono.MainWorld.GetResource<StartMenuDataResource>().IsAnySaveChanged = value;
		}
	}
}