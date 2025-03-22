using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "PrefabFctryConfig", menuName = "HungerOrHarvest/Config/View/PrefabFctryConfig")]
	public class PrefabFctryConfig : ScriptableObject {
		public List<Pair<VillType, GameObject>> VillPrefabs;
		public List<Pair<ArchType, GameObject>> ArchPrefabs;
		public List<Pair<LayerType, GameObject>> LayerPrefabs;

		private readonly Dictionary<ArchType, GameObject> _archs = new();
		private readonly Dictionary<LayerType, GameObject> _layers = new();
		private readonly Dictionary<VillType, GameObject> _vills = new();

		public void InitDict() {
			_archs.Clear();
			ArchPrefabs.ForEach( (pair) => _archs.Add(pair.Key, pair.Value) );
			_layers.Clear();
			LayerPrefabs.ForEach( (pair) => _layers.Add(pair.Key, pair.Value) );
			_vills.Clear();
			VillPrefabs.ForEach( (pair) => _vills.Add(pair.Key, pair.Value) );
		}

		public GameObject GetArchPrefab(ArchType type) {
			return _archs[type];
		}
		public GameObject GetVillPrefab(VillType type) {
			return _vills[type];
		}
		public GameObject GetLayerPrefab(LayerType type) {
			return _layers[type];
		}
	}
}