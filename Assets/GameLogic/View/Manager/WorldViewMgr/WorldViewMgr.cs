using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace GameLogic.View
{
	public class WorldViewMgr : MonoSingleton<WorldViewMgr>, IPlayerControll, IMananger {
		private readonly Dictionary<ulong, VillViewBase> _villViews = new();
		private readonly Dictionary<ulong, ArchViewBase> _archViews = new();
		private readonly Dictionary<ulong, LayerViewBase> _layerViews = new();

		protected override void Awake() {
			base.Awake();

			EventSystem.AddListener<VillLogicBase>((int)ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchAdded_A_1, OnArchAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<LayerLogicBase>((int)ModelEvt.LayerAdded_L_1, OnLayerAdded, NSFrame.EventType.Model);

			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchDestroyed_A_1, OnArchDestroyed, NSFrame.EventType.Model);
			EventSystem.AddListener<VillLogicBase>((int)ModelEvt.VillDestroyed_V_1, OnVillDestroyed, NSFrame.EventType.Model);
		}
		private void Start() {
			GameViewMgr.Inst.RegisterClearableMgr(this);
		}
		protected void OnDestroy() {
			GameViewMgr.Inst.UnregisterClearableMgr(this);

			EventSystem.RemoveListener<VillLogicBase>((int)ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.RemoveListener<ArchLogicBase>((int)ModelEvt.ArchAdded_A_1, OnArchAdded, NSFrame.EventType.Model);
			EventSystem.RemoveListener<LayerLogicBase>((int)ModelEvt.LayerAdded_L_1, OnLayerAdded, NSFrame.EventType.Model);

			EventSystem.RemoveListener<ArchLogicBase>((int)ModelEvt.ArchDestroyed_A_1, OnArchDestroyed, NSFrame.EventType.Model);
			EventSystem.RemoveListener<VillLogicBase>((int)ModelEvt.VillDestroyed_V_1, OnVillDestroyed, NSFrame.EventType.Model);
		}

		private void Update() {
			if (!Controllable) return;
			if (Input.GetKeyDown(KeyCode.Space)) {
				Controller.CmdRunner.Run("/pause");
			}
		}

		private void OnVillAdded(VillLogicBase vill) {
			var view = PrefabFctry.Inst.NewVillView(vill);
			_villViews.Add(vill.ID, view);
			view.SetSortingLayerIDbyY(vill.Coord.Y);
		}
		private void OnArchAdded(ArchLogicBase arch) {
			var view = PrefabFctry.Inst.NewArchView(arch);
			_archViews.Add(arch.ID, view);
			view.SetSortingLayerID(arch.OL.LYR);
		}
		private void OnLayerAdded(LayerLogicBase layer) {
			var view = PrefabFctry.Inst.NewLayerView(layer);
			_layerViews.Add(layer.ID, view);
			view.SetSortingLayerID(layer.LYR);
		}

		private void OnVillDestroyed(VillLogicBase vill) {
			if (_villViews.Remove(vill.ID, out var view)) {
				Destroy(view.gameObject);
			}
		}
		private void OnArchDestroyed(ArchLogicBase arch) {
			if (_archViews.Remove(arch.ID, out var view)) {
				Destroy(view.gameObject);
			}
		}

		#region PublicMethods
		public bool TryGetVillView(ulong id, out VillViewBase vView) {
			return _villViews.TryGetValue(id, out vView);
		}
		public VillViewBase FindVillView(ulong id) { return _villViews[id]; }
		public bool TryGetArchView(ulong id, out ArchViewBase aView) {
			return _archViews.TryGetValue(id, out aView);
		}
		public ArchViewBase FindArchView(ulong id) { return _archViews[id]; }
		public List<ArchViewBase> GetAllArchViews(ArchType archType) {
			var list = new List<ArchViewBase>();
			foreach (var view in _archViews.Values) { if (view.Logic.ArchType == archType) { list.Add(view); } }
			return list;
		}
		#endregion

		#region IClearMgr
		public void ClearMgr() {
			foreach (var view in _villViews.Values) { Destroy(view.gameObject); }
			_villViews.Clear();
			foreach (var view in _archViews.Values) { Destroy(view.gameObject); }
			_archViews.Clear();
			foreach (var view in _layerViews.Values) { Destroy(view.gameObject); }
			_layerViews.Clear();
		}
		#endregion

		#region IPlayerControll
		public bool Controllable { get; set; } = true;
		#endregion
	}
}