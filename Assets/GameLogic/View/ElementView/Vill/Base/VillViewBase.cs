using NSFrame;
using UnityEngine;

namespace GameLogic
{
	[RequireComponent(typeof(SmoothFade))]
	public abstract class VillViewBase : MonoBehaviour {

		private VillLogicBase _villLogic;
		public SmoothFade SmoothFade { get; private set; }
		public SmoothMove SmoothMove { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }

		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
			SmoothFade = GetComponent<SmoothFade>();
			SmoothMove = GetComponent<SmoothMove>();
			SmoothMove.Configs[0].Time = ConstMgr.Inst.Config.VILL_ONE_MOVE_TICK * TickTrigger.Inst.TickTime;
			SpriteRenderer.sortingOrder = ViewConstMgr.VILL_SORTING_ORDER;
		}

		public void SetVill(VillLogicBase vill) {
			_villLogic = vill;
			_villLogic.OnCoordChange += OnLogicCoordChange;
		}

		private void OnLogicCoordChange(Coord dlt) {
			var ori = _villLogic.Coord - dlt;
			if (ori.IsOnLayer() && dlt.Y != 0) {
				SpriteRenderer.sortingOrder = ViewConstMgr.MAX_SORTING_ORDER;
				if (dlt.Y == 1) {
					SetSortingLayerID(ori.Y / ConstMgr.Y_PER_LYR + 1);
				}
			}
			if (_villLogic.Coord.IsOnLayer()) {
				if (dlt.Y != 0) {
					SpriteRenderer.sortingOrder = ViewConstMgr.VILL_SORTING_ORDER;
					if (dlt.Y == -1) {
						SetSortingLayerID(_villLogic.Coord.Y / ConstMgr.Y_PER_LYR);
					}
				}
			}
			SmoothMove.SetTarget(_villLogic.Coord.ToViewCoord());
		}

		public void SetSortingLayerID(int lyr) {
			SpriteRenderer.sortingLayerID = SortingLayer.NameToID("m_Layer" + (ConstMgr.MAX_LYR + lyr));
		}
		
	}
}