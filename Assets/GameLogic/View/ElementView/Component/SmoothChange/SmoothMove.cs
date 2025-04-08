using UnityEngine;

namespace GameLogic.View
{
	public class SmoothMove : SmoothChangeBase<Vector3>{
		public override Vector3 GetCurVal() {
			return transform.localPosition;
		}

		protected override void SetCurVal_Derived(Vector3 val) {
			transform.localPosition = val;
		}
		protected override Vector3 Add(Vector3 lhv, Vector3 rhv) => lhv + rhv;

		protected override Vector3 Mul(Vector3 lhv, float rhv) => lhv * rhv;
		protected override Vector3 Sub(Vector3 lhv, Vector3 rhv) => lhv - rhv;
	}
}