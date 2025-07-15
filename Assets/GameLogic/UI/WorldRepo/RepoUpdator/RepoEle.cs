using System;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.UI.WorldRepo {
	public class RepoEle : MonoBehaviour, IGroupLayoutEle {
		private Image _image;
		private RectTransform _rectTrans;
		[SerializeField] private TextMeshProUGUI _sumText, _maxText;

		private void Awake() {
			_image = transform.GetChild(0).GetComponent<Image>();
			_rectTrans = GetComponent<RectTransform>();
		}

		#region PublicMethods
		public void SetIcon(Sprite sprite) {
			_image.sprite = sprite;
		}
		public void SetSumMax(float sum, float max) {
			_sumText.text = $"{sum:F1}";
			_maxText.text = $"{max:F1}";
		}
		#endregion

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public float EleSize => 100;
		public RectTransform RectTrans => _rectTrans;
#pragma warning disable 67
		public event Action OnDirty;
#pragma warning restore 67
		public void OnAddedToGroup() {
			_rectTrans.offsetMin = new(0, 0);
			_rectTrans.offsetMax = new(0, 0);
		}
		public void SetPos(float y) {
			_rectTrans.offsetMin = new(0, -y - EleSize);
			_rectTrans.offsetMax = new(0, -y);
		}
		public void LogicDestroy() { }
		#endregion
	}
}