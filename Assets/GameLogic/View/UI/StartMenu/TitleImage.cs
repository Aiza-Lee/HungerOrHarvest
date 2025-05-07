using UnityEngine;

namespace GameLogic.View.UI.StartMenu
{
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
		private void SetTask() {
			_isMax = !_isMax;
			_smoothScale
				.SetDoneCallback(() => SetTask())
				.SetTarget(_isMax ? new(_minScale, _minScale, _minScale) : new(_maxScale, _maxScale, _maxScale));
		}
	}
}