using OldGameLogic.Model.Element.Layer;
using OldGameLogic.Model.Mgr;
using UnityEngine;

namespace OldGameLogic.View
{
	public abstract class LayerViewBase : MonoBehaviour {
		private LayerLogicBase _layerLogic;
		public SpriteRenderer[] SpriteRenderers { get; private set; }

		private void Awake() {
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