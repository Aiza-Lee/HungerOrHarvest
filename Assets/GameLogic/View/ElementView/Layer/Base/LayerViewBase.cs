using UnityEngine;

namespace GameLogic
{
	public abstract class LayerViewBase : MonoBehaviour {
		private LayerLogicBase _layerLogic;
		public SmoothFade[] SmoothFades { get; private set; }
		public SpriteRenderer[] SpriteRenderers { get; private set; }

		private void Awake() {
			SmoothFades = GetComponents<SmoothFade>();
			SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
			foreach (var sr in SpriteRenderers) {
				sr.sortingOrder = ViewConstMgr.FRONT_SORTING_ORDER;
			}
		}

		public void SetLayer(LayerLogicBase layer) {
			_layerLogic = layer;
		}
		public void SetSortingLayerID(int lyr) {
			var SLID = SortingLayer.NameToID("m_Layer" + (ConstMgr.MAX_LYR + lyr));
			foreach (var sr in SpriteRenderers) {
				sr.sortingLayerID = SLID;
			}
		}
	}
}