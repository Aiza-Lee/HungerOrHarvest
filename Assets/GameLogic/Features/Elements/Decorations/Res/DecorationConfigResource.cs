using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Decorations {
	public enum DecorationType {
		Tree = 1,
		Stump = 2,
		Rock = 3,
	}
	[System.Serializable]
	public class DecorationConfigResource : IResource {
		public List<SerializablePair<DecorationType, List<GameObject>>> DecorationPrefabs;

		public GameObject GetRandomDecorationPrefab(DecorationType type) {
			var pair = DecorationPrefabs.Find(p => p.Key == type);
			if (pair != null && pair.Value.Count > 0) {
				return pair.Value[Random.Range(0, pair.Value.Count)];
			}
			return null;
		}
	}
}