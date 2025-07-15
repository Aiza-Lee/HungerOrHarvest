using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.PercentBar {
	
	/// <summary>
	/// 通用的百分比进度条类
	/// </summary>
	public class PrecentageBar : MonoBehaviour {

		[SerializeField] private RectTransform _barBack;
		[SerializeField] private RectTransform _barInner;

		private float ExpBarWidth => _barBack.rect.width;

		void OnEnable() {
			SetPercentage(0f);
		}

		public void SetPercentage(float percentage) {
			_barInner.offsetMax = new(-(1f - percentage) * ExpBarWidth, 0);
		}
	}
}