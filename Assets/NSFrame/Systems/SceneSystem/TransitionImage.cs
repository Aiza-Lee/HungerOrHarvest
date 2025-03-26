using UnityEngine;
using UnityEngine.UI;

namespace NSFrame {
	public class TransitionImage : PanelBase {
		public Image Image;
		private RectTransform _rectTransform;
		protected override void Awake() {
			base.Awake();
			_rectTransform = GetComponent<RectTransform>();
			_rectTransform.offsetMin = new(0, 0);
			_rectTransform.offsetMax = new(0, 0);
		}
		public override void OnClose() {}
		public override void OnShow() {}
	}
}