using GameLogic.UI.Common.UiComponents.SmoothChange;
using UnityEngine;

namespace GameLogic.UI.StartMenu.Title {
	[RequireComponent(typeof(SmoothScale))]
	public class TitleImage : MonoBehaviour {
		private SmoothScale _smoothScale;
		[SerializeField] private float _minScale;
		[SerializeField] private float _maxScale;
		private bool _isMax;
		private void Awake() {
			_smoothScale = GetComponent<SmoothScale>();
		}
		private void Start() {
			SetTask();
		}
		/// <summary>
		/// 实现循环放大缩小的功能
		/// </summary>
		private void SetTask() {
			_isMax = !_isMax;
			_smoothScale
				.SetDoneCallback(() => SetTask())
				.SetChangeInfoIndex(0)
				.SetTarget(_isMax ? new(_minScale, _minScale, _minScale) : new(_maxScale, _maxScale, _maxScale));
		}
	}
}