using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class WorldViewMgr : MonoSingleton<WorldViewMgr>, IPlayerControll {
		protected override void Awake() {
			base.Awake();
			EventSystem.AddListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			EventSystem.AddListener<LayerLogicBase>((int)LogicEvt.LayerAdded_L, OnLayerAdded);
		}

		private void Update() {
			if (!Controllable) return;
			if (Input.GetKeyDown(KeyCode.Space)) {
				CmdRunner.Run("/pause");
			}
		}

		private void OnVillAdded(VillLogicBase vill) {
			var view = PrefabFctry.Inst.NewVillView(vill);
			if (vill.Coord.IsOnLayer()) {
				view.SetSortingLayerID(vill.Coord.Y / ConstMgr.Y_PER_LYR);
			} else {
				view.SetSortingLayerID(Mathf.FloorToInt(1f * vill.Coord.Y / ConstMgr.Y_PER_LYR));
			}
		}
		private void OnArchAdded(ArchLogicBase arch) {
			var view = PrefabFctry.Inst.NewArchView(arch);
			view.SetSortingLayerID(arch.OL.LYR);
		}
		private void OnLayerAdded(LayerLogicBase layer) {
			var view = PrefabFctry.Inst.NewLayerView(layer);
			view.SetSortingLayerID(layer.LYR);
		}

		#region IPlayerControll
		public bool Controllable { get; set; } = true;
		#endregion
	}
}