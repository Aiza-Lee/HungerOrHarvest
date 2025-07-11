using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.NewWorldCreator {
	[System.Serializable]
	public class RandomWorldConfigResource : IResource {
		public List<RandomWorldBaseInfo> RandomWorldBaseInfos = new();

		public RandomWorldBaseInfo GetRandomWorldBaseInfo() {
			if (RandomWorldBaseInfos.Count == 0) {
				return null;
			}
			int randomIndex = UnityEngine.Random.Range(0, RandomWorldBaseInfos.Count);
			return RandomWorldBaseInfos[randomIndex];
		}
	}
}