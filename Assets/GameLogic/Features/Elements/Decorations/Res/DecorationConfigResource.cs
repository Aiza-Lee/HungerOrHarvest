using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Decorations {
	public enum DecorationType {
		Tree_01 = 1,
		Tree_02 = 2,
		Tree_03 = 3,
		Tree_04 = 4,
		Stump_01 = 101,
		Stump_02 = 102,
		Stump_03 = 103,
		Stump_04 = 104,
	}
	[System.Serializable]
	public class DecorationConfigResource : IResource {
		public List<SerializablePair<DecorationType, GameObject>> DecorationPrefabs;

		public GameObject GetDecorationPrefab(DecorationType type) {
			var pair = DecorationPrefabs.Find(p => p.Key == type);
			if (pair != null) {
				return pair.Value;
			}
			return null;
		}
	}
}