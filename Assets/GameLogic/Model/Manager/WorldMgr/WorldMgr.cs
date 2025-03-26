using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class WorldMgr : ISaveable<WorldSave> {
		private WorldMgr() {
			EventSystem.AddListener((int) LogicEvt.InitAllManager, () => {
				MaxArchODR = MinArchODR = 0;
				_maxUnlockedLayer = _minUnlockedLayer = 0;
				EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
				EventSystem.AddListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
				EventSystem.AddListener<LayerLogicBase>((int)LogicEvt.LayerAdded_L, OnLayerAdded);
			});
		}
		~WorldMgr() {
			EventSystem.RemoveListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			EventSystem.RemoveListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.RemoveListener<LayerLogicBase>((int)LogicEvt.LayerAdded_L, OnLayerAdded);
		}
		public static WorldMgr Inst { get; private set; } = new();
		
		private readonly List<VillLogicBase> _vills = new();
		private readonly List<ArchLogicBase> _archs = new();
		private readonly List<LayerLogicBase> _layers = new();
		private bool _layersOrdered = false;
		private readonly Dictionary<ulong, VillLogicBase> _villDict = new();
		private readonly Dictionary<ulong, ArchLogicBase> _archsDict = new();
		private readonly Dictionary<int, Pair<int, int>> _olRange = new();
		private int _maxUnlockedLayer;
		private int _minUnlockedLayer;


		public List<VillLogicBase> GetAllVills => _vills;
		public List<ArchLogicBase> GetAllArchs => _archs;
		public List<LayerLogicBase> GetAllLayers => _layers;
		public int MaxUnlockedLayer => _maxUnlockedLayer;
		public int MinUnlockedLayer => _minUnlockedLayer;
		public int MaxArchODR { get; private set; }
		public int MinArchODR { get; private set; }
		/// <summary>
		/// 地图的正向边缘
		/// </summary>
		public int LayerPosEdge { get; private set; }
		/// <summary>
		/// 地图的负向边缘
		/// </summary>
		public int LayerNegEdge { get; private set; }

		private void OnArchAdded(ArchLogicBase arch) {
			_archs.Add(arch);
			_archsDict.Add(arch.ID, arch);
			MaxArchODR = Mathf.Max(MaxArchODR, arch.OL.ODR);
			MinArchODR = Mathf.Min(MinArchODR, arch.OL.ODR);
		}
		private void OnVillAdded(VillLogicBase vill) {
			_vills.Add(vill);
			_villDict.Add(vill.ID, vill);
		}
		private void OnLayerAdded(LayerLogicBase layer) {
			_layers.Add(layer);
			LayerPosEdge = Mathf.Max(layer.LYR, LayerPosEdge);
			LayerNegEdge = Mathf.Min(layer.LYR, LayerNegEdge);
		}


		#region PublicMethods
		public VillLogicBase FindVill(ulong id) {
			if (_villDict.TryGetValue(id, out var vill)) { return vill; }
			Debug.LogError($"id:{id} has no matched Vill");
			return null;
		}
		public ArchLogicBase FindArch(ulong id) {
			if (_archsDict.TryGetValue(id, out var arch)) { return arch; }
			Debug.LogError($"id:{id} has no matched Arch");
			return null;
		}
		public LayerLogicBase FindLayer(int lyr) {
			if (lyr < LayerNegEdge || lyr > LayerPosEdge) { Debug.LogError("lyr out of range"); return null; }
			if (_layersOrdered) {
				return _layers[lyr - LayerNegEdge];
			} else {
				_layers.Sort(LayerComparer.Inst);
				_layersOrdered = true;
				return _layers[lyr - LayerNegEdge];
			}
		}

		public Pair<int, int> GetLayerRange(int lyr) {
			if (_olRange.TryGetValue(lyr, out var range)) { return range.Clone(); }
			return new Pair<int, int>(0, -1);
		}
		public bool IsOLUnlocked(OL ol) {
			if (_olRange.TryGetValue(ol.LYR, out var pair)) {
				return ol.ODR >= pair.Key && ol.ODR <= pair.Value;
			}
			return false;
		}
		public bool IsLayerUnlocked(int lyr) {
			return _olRange.ContainsKey(lyr);
		}
		public void UnlockOL(OL ol) {
			if (IsOLUnlocked(ol)) {
				Debug.LogWarning("unlock an already unlocked OL");
				return;
			}
			_maxUnlockedLayer = Mathf.Max(ol.LYR, MaxUnlockedLayer);
			_minUnlockedLayer = Mathf.Min(ol.LYR, MinUnlockedLayer);
			if (_olRange.TryGetValue(ol.LYR, out var range)) {
				range.Key = Mathf.Min(range.Key, ol.ODR);
				range.Value = Mathf.Max(range.Value, ol.ODR);
			} else {
				_olRange.Add(ol.LYR, new Pair<int, int>(ol.ODR, ol.ODR));
			}
			EventSystem.Invoke<OL>((int)LogicEvt.UnlockOL_O, ol);
		}
		#endregion

		#region ISaveable
		public WorldSave GetSave() {
			var save = new WorldSave {
				VillSaves = new(),
				ArchSaves = new(),
				LayerSaves = new(),
				OL_Range = new(),
				MaxUnlockedLayer = _maxUnlockedLayer,
				MinUnlockedLayer = _minUnlockedLayer,
			};
			foreach (var v in _vills) save.VillSaves.Add(v.GetSave());
			foreach (var a in _archs) save.ArchSaves.Add(a.GetSave());
			foreach (var l in _layers) save.LayerSaves.Add(l.GetSave());
			foreach (var pair in _olRange) save.OL_Range.Add(new(pair.Key, pair.Value.Clone()));
			return save;
		}
		public void InitFromSave(WorldSave save) {
			ClearCache();
			
			save.VillSaves.ForEach( (save) => LogicFctry.Inst.LoadVill(save) );
			save.ArchSaves.ForEach( (save) => LogicFctry.Inst.LoadArch(save) );
			save.LayerSaves.ForEach( (save) => LogicFctry.Inst.LoadLayer(save) );
			save.OL_Range.ForEach( (pair) => _olRange.Add(pair.Key, pair.Value) );

			_maxUnlockedLayer = save.MaxUnlockedLayer;
			_minUnlockedLayer = save.MinUnlockedLayer;
		}
		private void ClearCache() {
			_vills.Clear();
			_archs.Clear();
			_layers.Clear();
			_villDict.Clear();
			_archsDict.Clear();
			_olRange.Clear();
		}
		#endregion
	}
}