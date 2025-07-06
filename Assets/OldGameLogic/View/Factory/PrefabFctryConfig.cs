using System;
using System.Collections.Generic;
using UnityEngine;

namespace OldGameLogic.View
{
	// [CreateAssetMenu(fileName = "PrefabFctryConfig", menuName = "HungerOrHarvest/Config/View/PrefabFctryConfig")]
	public class PrefabFctryConfig : ScriptableObject {
		[SerializeField] private List<Pair<string, GameObject>> VillPrefabs;
		[SerializeField] private List<Pair<string, GameObject>> ArchPrefabs;
		[SerializeField] private List<Pair<string, GameObject>> LayerPrefabs;

		private readonly Dictionary<ArchType, GameObject> _archs = new();
		private readonly Dictionary<LayerType, GameObject> _layers = new();
		private readonly Dictionary<VillType, GameObject> _vills = new();

		public void InitDict() {
			_archs.Clear();
			ArchPrefabs.ForEach( (pair) => _archs.Add(Enum.Parse<ArchType>(pair.Key), pair.Value) );
			_layers.Clear();
			LayerPrefabs.ForEach( (pair) => _layers.Add(Enum.Parse<LayerType>(pair.Key), pair.Value) );
			_vills.Clear();
			VillPrefabs.ForEach( (pair) => _vills.Add(Enum.Parse<VillType>(pair.Key), pair.Value) );
		}

		public GameObject GetArchPrefab(ArchType type) => _archs[type];
		public GameObject GetVillPrefab(VillType type) => _vills[type];
		public GameObject GetLayerPrefab(LayerType type) => _layers[type];
	}
}