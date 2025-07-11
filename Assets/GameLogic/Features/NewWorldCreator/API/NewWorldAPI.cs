using GameLogic.World;

namespace GameLogic.Features.NewWorldCreator {
	public static class NewWorldAPI {
		/// <summary>
		/// 创建一个新的随机世界，需要提供世界名称。
		/// </summary>
		/// <param name="worldName"></param>
		public static void NewRandomWorld(string worldName) {
			var res = GameWorldMono.MainWorld.GetResource<NewWorldInfoResource>();
			var baseInfo = GameWorldMono.MainWorld.GetResource<RandomWorldConfigResource>().GetRandomWorldBaseInfo();
			res.NewWorldInfo = new() {
				WorldName = worldName,
				BaseInfo = baseInfo
			};
		}
	}
}