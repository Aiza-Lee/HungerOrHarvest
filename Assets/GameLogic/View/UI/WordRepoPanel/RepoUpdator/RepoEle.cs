using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldRepoPanel
{
	public class RepoEle : MonoBehaviour, IGroupLayoutEle {
		private Image _image;
		private RectTransform _rectTrans;
		private TextMeshProUGUI _textMesh;

		private void Awake() {
			_image = transform.GetChild(0).GetComponent<Image>();
			_rectTrans = GetComponent<RectTransform>();
			_textMesh = GetComponentInChildren<TextMeshProUGUI>();
		}

		#region PublicMethods
		public void SetIcon(Sprite sprite) {
			_image.sprite = sprite;
		}
		public void SetSum(float sum) {
			_textMesh.text = sum.ToString();
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
			_rectTrans.offsetMin = new(0, - y - EleSize);
			_rectTrans.offsetMax = new(0, - y);
		}
		public void Clear() {}
		#endregion
	}
}