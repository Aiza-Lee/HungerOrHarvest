using GameLogic.UI.Common.UiComponents.SmoothChange;
using NSFrame;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.UI.Common.UiComponents.PrettyButton {
	[RequireComponent(typeof(SmoothOffsetMin), typeof(SmoothOffsetMax), typeof(SmoothScale))]
	public class PrettyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler,
		IPointerEnterHandler, IPointerExitHandler {

		[Header("挂载组件")]
		[SerializeField] protected Image _backImage;
		[SerializeField] protected Image _buttonIcon;
		[SerializeField] protected TextMeshProUGUI _buttonText;

		[Header("配置")]
		[SerializeField] protected ButtonThemeConfig _themeConfig;
		[SerializeField] protected ButtonInteractionConfig _interactionConfig;

		[Header("触发事件")]
		[SerializeField] protected UnityEvent _onClick;
		[SerializeField] protected UnityEvent _onLongPress;

		protected SmoothOffsetMin _soMin;
		protected SmoothOffsetMax _soMax;
		protected SmoothScale _smoothScale;
		protected RectTransform _rectTransform;
		protected Color _originalColor;
		protected Vector2 _originalOffsetMin;
		protected Vector2 _originalOffsetMax;
		protected Vector3 _originalScale;

		protected SmoothImageColor _backImageColor;
		private void Awake() {
			_soMin = GetComponent<SmoothOffsetMin>();
			_soMax = GetComponent<SmoothOffsetMax>();
			_smoothScale = GetComponent<SmoothScale>();
			_rectTransform = GetComponent<RectTransform>();
			_originalColor = _backImage.color;
			_originalOffsetMin = _rectTransform.offsetMin;
			_originalOffsetMax = _rectTransform.offsetMax;
			_originalScale = _rectTransform.localScale;

			if (!_backImage.TryGetComponent(out _backImageColor)) {
				_backImageColor = _backImage.gameObject.AddComponent<SmoothImageColor>();
			}
		}

		// 长按相关
		protected float _pressStartTime;

		public bool Interactable {
			get => _interactionConfig.Interactable;
			set {
				if (_interactionConfig.Interactable == value) return;
				_interactionConfig.Interactable = value;
				if (_themeConfig.EnableColor) {
					_backImage.color = value ? _originalColor : _themeConfig.DisabledColor;
				}
				ResetToOrigin();
			}
		}

		private void ResetToOrigin() {
			_backImage.color = _originalColor;
			_soMax.SetCurVal(_originalOffsetMax);
			_soMin.SetCurVal(_originalOffsetMin);
			_smoothScale.SetCurVal(_originalScale);
		}

		public void OnPointerClick(PointerEventData eventData) {
			if (!Interactable) return;

			_onClick?.Invoke();
		}

		public void OnPointerDown(PointerEventData eventData) {
			if (!Interactable) return;

			if (_interactionConfig.EnableLongPress) {
				_pressStartTime = Time.unscaledTime;
			}
			if (_themeConfig.EnableScale) {
				_smoothScale.SetChangeInfo(_themeConfig.PressedScaleChangeInfo)
					.SetTarget(_themeConfig.PressedScale);
			}
			if (_themeConfig.EnableColor) {
				_backImageColor.SetChangeInfo(_themeConfig.ColorChangeInfo)
					.SetTarget(AlphaBlend(_themeConfig.PressedColor, _originalColor));
			}
			if (_themeConfig.EnablePressedOffset) {
				_soMin.SetChangeInfo(_themeConfig.PressedOffsetChangeInfo)
					.Translate(_themeConfig.PressedOffset);
				_soMax.SetChangeInfo(_themeConfig.PressedOffsetChangeInfo)
					.Translate(_themeConfig.PressedOffset);
			}
			if (_themeConfig.EnableAudio) {
				if (_themeConfig.ClickSound != null) {
					AudioSystem.PlaySFX(_themeConfig.ClickSound, _themeConfig.Volume);
				}
			}
			if (_themeConfig.EnableClickParticles) {
				if (_themeConfig.ClickParticlePrefab != null) {
					ParticleSystem particle = Instantiate(_themeConfig.ClickParticlePrefab, _rectTransform);
					RectTransformUtility.ScreenPointToLocalPointInRectangle(
						_rectTransform, eventData.position, eventData.pressEventCamera,
						out Vector2 localPoint
					);
					particle.transform.localPosition = localPoint;
					particle.Play();
					Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constantMax);
				} else {
					Debug.LogWarning("Click Particle Prefab is not set in PrettyButton theme config.");
				}
			}
		}

		public void OnPointerUp(PointerEventData eventData) {
			if (!Interactable) return;


			if (_interactionConfig.EnableLongPress) {
				if (Time.unscaledTime - _pressStartTime >= _interactionConfig.LongPressTime) {
					_onLongPress?.Invoke();
				}
			}
			if (_themeConfig.EnableScale) {
				_smoothScale.SetChangeInfo(_themeConfig.UnpressedScaleChangeInfo)
					.SetTarget(Vector3.one);
			}
			if (_themeConfig.EnableColor) {
				_backImageColor.SetChangeInfo(_themeConfig.ColorChangeInfo)
					.SetTarget(_originalColor);
			}
			if (_themeConfig.EnablePressedOffset) {
				_soMin.SetChangeInfo(_themeConfig.UnpressedOffesetChangeInfo)
					.SetTarget(_originalOffsetMin);
				_soMax.SetChangeInfo(_themeConfig.UnpressedOffesetChangeInfo)
					.SetTarget(_originalOffsetMax);
			}
		}

		public void OnPointerEnter(PointerEventData eventData) {
			if (!Interactable) return;

			if (_themeConfig.EnableColor) {
				_backImageColor.SetChangeInfo(_themeConfig.ColorChangeInfo)
					.SetTarget(AlphaBlend(_themeConfig.HoverColor, _originalColor));
			}
			if (_themeConfig.EnableScale) {
				_smoothScale.SetChangeInfo(_themeConfig.HoverScaleChangeInfo)
					.SetTarget(_themeConfig.HoverScale);
			}
			if (_themeConfig.EnableAudio) {
				if (_themeConfig.HoverSound != null) {
					AudioSystem.PlaySFX(_themeConfig.HoverSound, _themeConfig.Volume);
				}
			}
		}

		public void OnPointerExit(PointerEventData eventData) {
			if (!Interactable) return;

			if (_themeConfig.EnableColor) {
				_backImageColor.SetChangeInfo(_themeConfig.ColorChangeInfo)
					.SetTarget(_originalColor);
			}
			if (_themeConfig.EnableScale) {
				_smoothScale.SetChangeInfo(_themeConfig.UnhoverScaleChangeInfo)
					.SetTarget(Vector3.one);
			}
		}
		
		private Color AlphaBlend(Color front, Color back) {
			float r = front.r * front.a + back.r * (1 - front.a);
			float g = front.g * front.a + back.g * (1 - front.a);
			float b = front.b * front.a + back.b * (1 - front.a);
			float a = front.a * front.a + back.a * (1 - front.a);
			return new(r, g, b, a);
		}
	}
}