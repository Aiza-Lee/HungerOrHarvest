using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class WorldViewMgr : MonoSingleton<WorldViewMgr>, IPlayerControll {
		private readonly Dictionary<ulong, VillViewBase> _villViews = new();
		private readonly Dictionary<ulong, ArchViewBase> _archViews = new();
		private readonly Dictionary<ulong, LayerViewBase> _layerViews = new();

		protected override void Awake() {
			base.Awake();
			EventSystem.AddListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			EventSystem.AddListener<LayerLogicBase>((int)LogicEvt.LayerAdded_L, OnLayerAdded);

			EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchDestroyed_A, OnArchDestroyed);
			EventSystem.AddListener<VillLogicBase>((int)LogicEvt.VillDestroyed_V, OnVillDestroyed);
		}
		protected void OnDestroy() {
			EventSystem.RemoveListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.RemoveListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			EventSystem.RemoveListener<LayerLogicBase>((int)LogicEvt.LayerAdded_L, OnLayerAdded);

			EventSystem.RemoveListener<ArchLogicBase>((int)LogicEvt.ArchDestroyed_A, OnArchDestroyed);
			EventSystem.RemoveListener<VillLogicBase>((int)LogicEvt.VillDestroyed_V, OnVillDestroyed);
		}

		private void Update() {
			if (!Controllable) return;
			if (Input.GetKeyDown(KeyCode.Space)) {
				CmdRunner.Run("/pause");
			}
		}

		private void OnVillAdded(VillLogicBase vill) {
			var view = PrefabFctry.Inst.NewVillView(vill);
			_villViews.Add(vill.ID, view);
			if (vill.Coord.IsOnLayer()) {
				view.SetSortingLayerID(vill.Coord.Y / ConstMgr.Y_PER_LYR);
			} else {
				view.SetSortingLayerID(Mathf.FloorToInt(1f * vill.Coord.Y / ConstMgr.Y_PER_LYR));
			}
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
		public bool TryGetArchView(ulong id, out ArchViewBase aView) {
			return _archViews.TryGetValue(id, out aView);
		}
		#endregion

		#region IPlayerControll
		public bool Controllable { get; set; } = true;
		#endregion
	}
}