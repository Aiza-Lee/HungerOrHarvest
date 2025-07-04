using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Model.Element.Layer;
using OldGameLogic.Model.Element.Vill;
using OldGameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.View
{
	public class PrefabFctry : MonoSingleton<PrefabFctry> {
		public PrefabFctryConfig Config;
		protected override void Awake() {
			base.Awake();
			Config.InitDict();
		}
		public VillViewBase NewVillView(VillLogicBase villLogic) {
			var prefab = Config.GetVillPrefab(villLogic.VillType);
			var go = GameObject.Instantiate(prefab);
			go.transform.position = villLogic.Coord.ToViewCoord();
			var view = go.GetComponent<VillViewBase>();
			view.SetVill(villLogic);
			return view;
		}

		public ArchViewBase NewArchView(ArchLogicBase archLogic) {
			var prefab = Config.GetArchPrefab(archLogic.ArchType);
			var go = GameObject.Instantiate(prefab);
			go.transform.position = archLogic.OL.ToViewCoord();
			var view = go.GetComponent<ArchViewBase>();
			view.SetArch(archLogic);
			return view;
		}

		public LayerViewBase NewLayerView(LayerLogicBase layerLogic) {
			var prefab = Config.GetLayerPrefab(layerLogic.LayerType);
			var go = GameObject.Instantiate(prefab);
			go.transform.position = new OL(0, layerLogic.LYR).ToViewCoord();
			var view = go.GetComponent<LayerViewBase>();
			view.SetLayer(layerLogic);
			return view;
		}
	}
}