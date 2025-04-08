using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillGroup : MonoBehaviour {
		[Header("挂载")]
		public GameObject VillCardPrefab;
		public RectTransform InnerRect;

		public float Width => _rectTransform.rect.width;

		private float Space => InnerRect.offsetMin.x;
		private readonly List<VillCard> _villCards = new();

		public List<VillCard> VillCards => _villCards;

		private RectTransform _rectTransform;
		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}
		private void OnDisable() {
			_villCards.Clear();
		}

		#region Injection
		public void InjectInfo(ArchLogicBase arch = null) {
			if (arch != null) {
				foreach (var vID in arch.BondedVills) {
					AddVillCard(PoolSystem.PopGO<VillCard>(VillCardPrefab).InjectVillView(WorldViewMgr.Inst.FindVillView(vID)));
				}
			}
		}
 		#endregion

		private void AddVillCard(VillCard villCard) {
			villCard.transform.SetParent(InnerRect);
			villCard.OnSetedAsChild();
			float posx = _villCards.Count * (villCard.Width + Space);
			SetCardPos(villCard, posx);

			_villCards.Add(villCard);
			this.SetRightEdge(posx + villCard.Width + Space * 2);
		}
		private void RemoveVillCard(VillCard villCard) {
			_villCards.Remove(villCard);
			RearrangeCards();
		}
		private void RearrangeCards() {
			float pos = 0f;
			for (int i = 0; i < _villCards.Count; i++) {
				SetCardPos(_villCards[i], pos);
				pos += _villCards[i].Width + Space;
			}
			this.SetRightEdge(pos + Space);
		}
		private void SetCardPos(VillCard villCard, float pos) {
			villCard.SetRightEdge(pos + villCard.Width);
			villCard.SetLeftEdge(pos);
		}

		#region PublicMethods
		public void SetLeftEdge(float x) {
			_rectTransform.offsetMin = new(x, _rectTransform.offsetMin.y);
		}
		public void SetRightEdge(float x) {
			_rectTransform.offsetMax = new(x, _rectTransform.offsetMax.y);
		}
		public void OnSetedAsChild() {
			_rectTransform.offsetMin = new(0, 0);
			_rectTransform.offsetMax = new(Space * 2, 0);
		}
		#endregion
	}
}