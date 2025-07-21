using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.UI.Common.UiComponents.SmoothChange {
	public class SmoothImageColor : SmoothChangeBase<Color> {
		private Image _image;
		void Awake() {
			_image = GetComponent<Image>();
		}

		public override Color GetCurVal() {
			return _image.color;
		}

		protected override Color Add(Color lhv, Color rhv) {
			return lhv + rhv;
		}

		protected override Color Mul(Color lhv, float rhv) {
			return lhv * rhv;
		}

		protected override void SetCurVal_Derived(Color val) {
			_image.color = val;
		}

		protected override Color Sub(Color lhv, Color rhv) {
			return lhv - rhv;
		}
	}
}