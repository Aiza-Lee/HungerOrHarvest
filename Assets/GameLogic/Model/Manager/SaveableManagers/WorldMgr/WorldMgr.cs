using System.Collections.Generic;
using GameLogic.Model.Element.Arch;
using GameLogic.Model.Element.Vill;
using GameLogic.Model.Factory;
using GameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class WorldMgr : ISaveable<WorldSave>, IMananger {
		private WorldMgr() {
			MaxArchODR = MinArchODR = 0;
			_maxUnlockedLayer = _minUnlockedLayer = 0;

			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchAdded_A_1, OnArchAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<VillLogicBase>((int)ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<LayerLogicBase>((int)ModelEvt.LayerAdded_L_1, OnLayerAdded, NSFrame.EventType.Model);

			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchDestroyed_A_1, OnArchDestroy, NSFrame.EventType.Model);
			EventSystem.AddListener<VillLogicBase>((int)ModelEvt.VillDestroyed_V_1, OnVillDestroy, NSFrame.EventType.Model);
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

		private void OnArchDestroy(ArchLogicBase arch) {
			_archs.Remove(arch);
			_archsDict.Remove(arch.ID);
		}
		private void OnVillDestroy(VillLogicBase vill) {
			_vills.Remove(vill);
			_villDict.Remove(vill.ID);
		}


		#region PublicMethods
		public VillLogicBase FindVill(ulong id) {
			if (_villDict.TryGetValue(id, out var vill)) { return vill; }
			Debug.LogWarning($"id:{id} has no matched Vill");
			return null;
		}
		public ArchLogicBase FindArch(ulong id) {
			if (_archsDict.TryGetValue(id, out var arch)) { return arch; }
			Debug.LogWarning($"id:{id} has no matched Arch");
			return null;
		}
		public LayerLogicBase FindLayer(int lyr) {
			if (lyr < LayerNegEdge || lyr > LayerPosEdge) { Debug.LogWarning("lyr out of range"); return null; }
			if (_layersOrdered) {
				return _layers[lyr - LayerNegEdge];
			} else {
				_layers.Sort(LayerComparer.Inst);
				_layersOrdered = true;
				return _layers[lyr - LayerNegEdge];
			}
		}
		public List<ulong> GetHomelessVillIDs() {
			return _vills.FindAll(v => v.IsHomeless).ConvertAll(v => v.ID);
		}
		public List<ulong> GetWorklessVillIDs() {
			return _vills.FindAll(v => v.IsWorkless).ConvertAll(v => v.ID);
		}
		public bool IsAnyArch(ArchType archType) { return _archs.Exists(a => a.ArchType == archType); }

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
			EventSystem.Invoke<OL>((int)ModelEvt.UnlockOL_O_1, ol, NSFrame.EventType.Model);
		}

		public ulong FindWorkForVill(ArchType archType) {
			var arch = _archs.Find(a => a.ArchType == archType && a.CheckBondVill());
			return arch == null ? 0 : arch.ID;
		}
		public bool FindWorkForVill(int villCnt, ArchType archType) {
			int cnt = 0;
			foreach (var arch in _archs) {
				if (arch.ArchType == archType) {
					cnt += arch.Lconfig.MaxContain - arch.BondedVillCount;
					if (cnt >= villCnt) return true;
				}
			}
			return false;
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
			// note: 这里的顺序很重要，村民、TaskRunner、Task的 InitFromSave 中不能调用 FindArch
			save.VillSaves.ForEach( (save) => LogicFctry.Inst.LoadVill(save) );
			save.ArchSaves.ForEach( (save) => LogicFctry.Inst.LoadArch(save) );
			save.LayerSaves.ForEach( (save) => LogicFctry.Inst.LoadLayer(save) );
			save.OL_Range.ForEach( (pair) => _olRange.Add(pair.Key, pair.Value) );

			_maxUnlockedLayer = save.MaxUnlockedLayer;
			_minUnlockedLayer = save.MinUnlockedLayer;
		}
		#endregion

		public void ClearMgr() {
			_vills.Clear();
			_archs.Clear();
			_layers.Clear();
			_villDict.Clear();
			_archsDict.Clear();
			_olRange.Clear();
		}
	}
}