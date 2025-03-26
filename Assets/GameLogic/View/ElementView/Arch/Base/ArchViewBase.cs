using UnityEngine;

namespace GameLogic
{
	[RequireComponent(typeof(SmoothFade))]
	public abstract class ArchViewBase : MonoBehaviour {
		
		private ArchLogicBase _archLogic;
		
		public SpriteRenderer SpriteRenderer { get; private set; }

		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
			SpriteRenderer.sortingOrder = ViewConstMgr.ARCH_SORTING_ORDER;
		}

		public  void SetArch(ArchLogicBase arch) {
			_archLogic = arch;
		}
		public void SetSortingLayerID(int lyr) {
			SpriteRenderer.sortingLayerID = SortingLayer.NameToID("m_Layer" + (ConstMgr.MAX_LYR + lyr));
		}

	}
}