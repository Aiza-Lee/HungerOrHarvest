using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.SmoothChange {
	public class SmoothScale : SmoothChangeBase<Vector3> {
		private RectTransform _rectTrans;
		
		void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		public override Vector3 GetCurVal() => _rectTrans.localScale;
		protected override Vector3 Add(Vector3 lhv, Vector3 rhv) => lhv + rhv;
		protected override Vector3 Mul(Vector3 lhv, float rhv) => lhv * rhv;
		protected override void SetCurVal_Derived(Vector3 val) => _rectTrans.localScale = val;
		protected override Vector3 Sub(Vector3 lhv, Vector3 rhv) => lhv - rhv;	}
}