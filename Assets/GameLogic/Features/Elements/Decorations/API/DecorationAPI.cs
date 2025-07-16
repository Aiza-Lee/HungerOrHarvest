using GameLogic.World;
using UnityEngine;

namespace GameLogic.Features.Elements.Decorations {
	public static class DecorationAPI {
		/// <summary>
		/// 获取随机装饰物预制体
		/// </summary>
		/// <param name="type">装饰物类型</param>
		/// <returns>随机装饰物预制体</returns>
		public static GameObject GetRandomDecorationPrefab(DecorationType type) {
			return GameWorldMono.MainWorld.GetResource<DecorationConfigResource>().GetRandomDecorationPrefab(type);
		}
	}
}