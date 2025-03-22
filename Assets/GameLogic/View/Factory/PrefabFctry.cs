using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class PrefabFctry : MonoSingleton<PrefabFctry> {
		public PrefabFctryConfig Config;
		protected override void Awake() {
			base.Awake();
			Config.InitDict();
		}
		public VillViewBase NewVillView(VillLogicBase villLogic) {
			var prefab = Config.VillPrefabs.Find(p => p.Key == villLogic.VillType).Value;
			var go = GameObject.Instantiate(prefab);
			go.transform.position = villLogic.Coord.ToViewCoord();
			var view = go.GetComponent<VillViewBase>();
			view.SetVill(villLogic);
			return view;
		}

		public ArchViewBase NewArchView(ArchLogicBase archLogic) {
			var prefab = Config.ArchPrefabs.Find(p => p.Key == archLogic.ArchType).Value;
			var go = GameObject.Instantiate(prefab);
			go.transform.position = archLogic.OL.ToViewCoord();
			var view = go.GetComponent<ArchViewBase>();
			view.SetArch(archLogic);
			return view;
		}

		public LayerViewBase NewLayerView(LayerLogicBase layerLogic) {
			var prefab = Config.LayerPrefabs.Find(p => p.Key == layerLogic.LayerType).Value;
			var go = GameObject.Instantiate(prefab);
			go.transform.position = new OL(0, layerLogic.LYR).ToViewCoord();
			var view = go.GetComponent<LayerViewBase>();
			view.SetLayer(layerLogic);
			return view;
		}
	}
}