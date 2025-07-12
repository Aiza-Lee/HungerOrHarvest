using UnityEngine;

namespace GameLogic.UI.Common.UnityComponents.SmoothChange {
	[RequireComponent(typeof(RectTransform))]
	public class SmoothOffsetMin : SmoothChangeBase<Vector2> {
		private RectTransform _rectTransform;
		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}

		public override Vector2 GetCurVal() { return _rectTransform.offsetMin; }
		protected override void SetCurVal_Derived(Vector2 val) { _rectTransform.offsetMin = val; }
		protected override Vector2 Add(Vector2 lhv, Vector2 rhv) { return lhv + rhv; }
		protected override Vector2 Mul(Vector2 lhv, float rhv) { return lhv * rhv; }
		protected override Vector2 Sub(Vector2 lhv, Vector2 rhv) { return lhv - rhv; }
	}
}