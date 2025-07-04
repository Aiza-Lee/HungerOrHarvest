using OldGameLogic.Model.Element.Vill;
using OldGameLogic.Model.Mgr;
using OldGameLogic.Utilities;
using UnityEngine;

namespace OldGameLogic.View
{
	[RequireComponent(typeof(SmoothFade))]
	public abstract class VillViewBase : MonoBehaviour {

		private VillLogicBase _villLogic;
		private SmoothMove SmoothMove { get; set; }
		private SpriteRenderer SpriteRenderer { get; set; }

		public VillLogicBase Logic => _villLogic;
		public Sprite Sprite => SpriteRenderer.sprite;


		private void Awake() {
			SpriteRenderer = GetComponent<SpriteRenderer>();
			SmoothMove = GetComponent<SmoothMove>();
			SmoothMove.Configs[0].Time = ConfigMgr.Config.VILL_ONE_MOVE_TICK_NORMAL * TickTrigger.Inst.TickTime;
			SpriteRenderer.sortingOrder = ViewConstMgr.VILL_SORTING_ORDER;
		}
		private void OnDestroy() {
			_villLogic.OnCoordChange -= OnLogicCoordChange;
		}
		
		#region Injection
		public void SetVill(VillLogicBase vill) {
			_villLogic = vill;
			_villLogic.OnCoordChange += OnLogicCoordChange;
		}
		#endregion

		private void OnLogicCoordChange(Coord _) {
			SetSortingLayerIDbyY(_villLogic.Coord.Y);
			SmoothMove.SetTarget(_villLogic.Coord.ToViewCoord());
		}

		private void SetSortingLayerID(int lyr) {
			SpriteRenderer.sortingLayerID = SortingLayer.NameToID("m_Layer" + (ConstMgr.MAX_LYR + lyr));
		}

		#region PublicMethods
		public void SetSortingLayerIDbyY(int y) {
			SetSortingLayerID(Mathf.CeilToInt(1f * y / ConstMgr.Y_PER_LYR));
			if (y % ConstMgr.Y_PER_LYR == 0) {
				SpriteRenderer.sortingOrder = ViewConstMgr.VILL_SORTING_ORDER;
			} else {
				SpriteRenderer.sortingOrder = ViewConstMgr.MAX_SORTING_ORDER;
			}
		}
		#endregion

		
	}
}