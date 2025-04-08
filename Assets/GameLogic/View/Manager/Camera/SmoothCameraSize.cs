using UnityEngine;

namespace GameLogic.View
{
	[RequireComponent(typeof(Camera))]
	public class SmoothCameraSize : SmoothChangeBase<float> {
		private Camera _camera;

		private void Awake() {
			_camera = GetComponent<Camera>();
		}

		protected override float Add(float lhv, float rhv) => lhv + rhv;
		protected override float Mul(float lhv, float rhv) => lhv * rhv;
		protected override float Sub(float lhv, float rhv) => lhv - rhv;

		public override float GetCurVal() {
			return _camera.fieldOfView;
		}
		protected override void SetCurVal_Derived(float val) {
			_camera.fieldOfView = val;
		}
	}
}