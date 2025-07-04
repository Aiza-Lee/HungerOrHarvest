using System;
using System.Collections.Generic;
using System.Linq;
using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Model.Element.Layer;
using OldGameLogic.Model.Element.Vill;
using OldGameLogic.Model.Factory;
using OldGameLogic.Model.Mgr;
using OldGameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace OldGameLogic
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
		public IEnumerable<ArchLogicBase> FindAllArchs(ArchType archType) {
			return from arch in _archs
				   where arch.ArchType == archType
				   select arch;
		}
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
		/// <summary>
		/// 根据predicate查找符合条件的vill的id
		/// </summary>
		/// <param name="predicate">条件</param>
		public List<ulong> FindVillIDs(Func<VillLogicBase, bool> predicate) {
			return _vills.FindAll(v => predicate(v)).ConvertAll(v => v.ID);
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

		/// <summary>
		/// 为多个村民寻找工作，返回是否有这么多空位（工作建筑和Home都适用）
		/// </summary>
		/// <param name="villCnt">需要的工作数量</param>
		/// <param name="archType">建筑类型</param>
		public bool HaveBondPosForVills(int villCnt, ArchType archType) {
			var workcnt = 0;
			var archs = _archs.Where(arch => arch.ArchType == archType);
			foreach (var arch in archs) {
				workcnt += arch.RestBondPositions;
				if (workcnt >= villCnt) {
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 为村民寻找最近的工作点（相对于家的坐标）
		/// </summary>
		/// <param name="villID">村民ID</param>
		/// <param name="archType">建筑类型</param>
		/// <param name="workArch">找到的建筑，没找到则为null</param>
		/// <returns>成功找到返回true，失败返回false</returns>
		public bool FindWorkForVill(ulong villID, ArchType archType, out ArchLogicBase workArch) {
			var vill = FindVill(villID);
			var home = FindArch(vill.HomeID);
			// 寻找最近的
			var archs = from arch in _archs
						where arch.ArchType == archType && arch.CheckBondVill()
						orderby RouteMgr.Inst.GetRoute(home.Coord, arch.Coord).Count
						select arch;
			if (archs.Count() > 0) {
				workArch = archs.First();
				return true;
			}
			workArch = null;
			return false;
		}
		/// <summary>
		/// 为村民寻找随机的家
		/// </summary>
		/// <param name="villID">村民ID</param>
		/// <param name="cottage">找到的家，没找到的话为null</param>
		/// <returns>成功找到返回true，失败返回false</returns>
		public bool FindHomeForVill(ulong villID, out CottageLogic cottage) {
			var vill = FindVill(villID);
			var cottages = from c in _archs
						   where c.ArchType == ArchType.Cottage && c.CheckBondVill()
						   select c;
			if (cottages.Count() > 0) {
				cottage = cottages.First() as CottageLogic;
				return true;
			}
			cottage = null;
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
			// arch 和 vill 的 destroy 会自动（WorldMgr通过事件中心监听了销毁事件）从 _archs 和 _vills 中移除
			while (_archs.Count > 0) _archs[0].Destroy();
			while (_vills.Count > 0) _vills[0].LogicDestroy();
			_layers.Clear();
			_olRange.Clear();
		}
	}
}