using System;
using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.ShakeEffect {
	/// <summary>
	/// 独立的抖动效果组件，可以应用到任何Transform上
	/// </summary>
	public class ShakeEffect : MonoBehaviour {
		[Header("Shake Settings")]
		[SerializeField] private float _defaultDuration = 1f;
		[SerializeField] private float _defaultMagnitude = 1f;
		[SerializeField] private float _defaultFrequency = 10f;
		[SerializeField] private AnimationCurve _decayCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

		[Header("Shake Type")]
		[SerializeField] private ShakeType _shakeType = ShakeType.Position;
		[SerializeField] private ShakeAxis _shakeAxis = ShakeAxis.XY;

		[Header("Advanced Settings")]
		[SerializeField] private bool _useUnscaledTime = false;
		[SerializeField] private bool _randomPhase = true;

		// Runtime variables
		private bool _isShaking = false;
		private float _elapsedTime = 0f;
		private float _duration = 1f;
		private float _magnitude = 1f;
		private float _frequency = 10f;
		private Vector3 _originalValue;
		private Vector3 _phaseOffset;

		// Callbacks
		private Action _onShakeComplete;
		private Action<Vector3> _onShakeUpdate;

		// Transform cache
		private Transform _transform;

		public enum ShakeType {
			Position,
			LocalPosition,
			Scale,
			Rotation
		}

		public enum ShakeAxis {
			X, Y, Z,
			XY, XZ, YZ,
			XYZ
		}

		// Public Properties
		public bool IsShaking => _isShaking;
		public float Progress => _duration > 0 ? Mathf.Clamp01(_elapsedTime / _duration) : 1f;
		public Vector3 CurrentShakeOffset { get; private set; }

		private void Awake() {
			_transform = transform;
			if (_randomPhase) {
				_phaseOffset = new Vector3(
					UnityEngine.Random.Range(0f, 2f * Mathf.PI),
					UnityEngine.Random.Range(0f, 2f * Mathf.PI),
					UnityEngine.Random.Range(0f, 2f * Mathf.PI)
				);
			}
		}

		private void Update() {
			if (_isShaking) {
				UpdateShake();
			}
		}

		private void UpdateShake() {
			float deltaTime = _useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
			_elapsedTime += deltaTime;

			if (_elapsedTime >= _duration) {
				StopShake();
				return;
			}

			// Calculate shake offset
			float progress = _elapsedTime / _duration;
			float decayFactor = _decayCurve.Evaluate(progress);

			Vector3 shakeOffset = CalculateShakeOffset(_elapsedTime, decayFactor);
			CurrentShakeOffset = shakeOffset;

			// Apply shake based on type
			ApplyShake(_originalValue + shakeOffset);

			// Trigger update callback
			_onShakeUpdate?.Invoke(shakeOffset);
		}

		private Vector3 CalculateShakeOffset(float time, float decayFactor) {
			Vector3 offset = Vector3.zero;

			// Generate noise-based shake for more natural effect
			float noiseScale = _frequency * 0.1f;

			switch (_shakeAxis) {
				case ShakeAxis.X:
					offset.x = GetShakeValue(time, _phaseOffset.x, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.Y:
					offset.y = GetShakeValue(time, _phaseOffset.y, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.Z:
					offset.z = GetShakeValue(time, _phaseOffset.z, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.XY:
					offset.x = GetShakeValue(time, _phaseOffset.x, noiseScale) * _magnitude * decayFactor;
					offset.y = GetShakeValue(time, _phaseOffset.y, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.XZ:
					offset.x = GetShakeValue(time, _phaseOffset.x, noiseScale) * _magnitude * decayFactor;
					offset.z = GetShakeValue(time, _phaseOffset.z, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.YZ:
					offset.y = GetShakeValue(time, _phaseOffset.y, noiseScale) * _magnitude * decayFactor;
					offset.z = GetShakeValue(time, _phaseOffset.z, noiseScale) * _magnitude * decayFactor;
					break;
				case ShakeAxis.XYZ:
					offset.x = GetShakeValue(time, _phaseOffset.x, noiseScale) * _magnitude * decayFactor;
					offset.y = GetShakeValue(time, _phaseOffset.y, noiseScale) * _magnitude * decayFactor;
					offset.z = GetShakeValue(time, _phaseOffset.z, noiseScale) * _magnitude * decayFactor;
					break;
			}

			return offset;
		}

		private float GetShakeValue(float time, float phase, float noiseScale) {
			// Combine sine wave with Perlin noise for more natural shake
			float sineWave = Mathf.Sin(time * _frequency * 2 * Mathf.PI + phase);
			float noise = (Mathf.PerlinNoise(time * noiseScale, phase) - 0.5f) * 2f;
			return (sineWave * 0.7f + noise * 0.3f);
		}

		private void ApplyShake(Vector3 value) {
			switch (_shakeType) {
				case ShakeType.Position:
					_transform.position = value;
					break;
				case ShakeType.LocalPosition:
					_transform.localPosition = value;
					break;
				case ShakeType.Scale:
					_transform.localScale = value;
					break;
				case ShakeType.Rotation:
					_transform.rotation = Quaternion.Euler(value);
					break;
			}
		}

		private Vector3 GetCurrentValue() {
			return _shakeType switch {
				ShakeType.Position => _transform.position,
				ShakeType.LocalPosition => _transform.localPosition,
				ShakeType.Scale => _transform.localScale,
				ShakeType.Rotation => _transform.rotation.eulerAngles,
				_ => Vector3.zero,
			};
		}

		#region Public Methods

		/// <summary>
		/// 开始抖动效果
		/// </summary>
		public ShakeEffect StartShake() {
			return StartShake(_defaultDuration, _defaultMagnitude, _defaultFrequency);
		}

		/// <summary>
		/// 开始抖动效果，使用自定义参数
		/// </summary>
		public ShakeEffect StartShake(float duration, float magnitude = -1f, float frequency = -1f) {
			if (_isShaking) {
				StopShake();
			}

			_duration = duration;
			_magnitude = magnitude > 0 ? magnitude : _defaultMagnitude;
			_frequency = frequency > 0 ? frequency : _defaultFrequency;

			_originalValue = GetCurrentValue();
			_elapsedTime = 0f;
			_isShaking = true;
			CurrentShakeOffset = Vector3.zero;

			return this;
		}

		/// <summary>
		/// 停止抖动效果
		/// </summary>
		public ShakeEffect StopShake() {
			if (!_isShaking) return this;

			_isShaking = false;
			_elapsedTime = 0f;
			CurrentShakeOffset = Vector3.zero;

			ApplyShake(_originalValue);

			_onShakeComplete?.Invoke();
			_onShakeComplete = null;

			return this;
		}

		/// <summary>
		/// 设置抖动完成回调
		/// </summary>
		public ShakeEffect SetOnComplete(Action callback) {
			_onShakeComplete = callback;
			return this;
		}

		/// <summary>
		/// 设置抖动更新回调
		/// </summary>
		public ShakeEffect SetOnUpdate(Action<Vector3> callback) {
			_onShakeUpdate = callback;
			return this;
		}

		/// <summary>
		/// 设置抖动类型
		/// </summary>
		public ShakeEffect SetShakeType(ShakeType type) {
			if (!_isShaking) {
				_shakeType = type;
			}
			return this;
		}

		/// <summary>
		/// 设置抖动轴向
		/// </summary>
		public ShakeEffect SetShakeAxis(ShakeAxis axis) {
			_shakeAxis = axis;
			return this;
		}

		/// <summary>
		/// 设置衰减曲线
		/// </summary>
		public ShakeEffect SetDecayCurve(AnimationCurve curve) {
			_decayCurve = curve;
			return this;
		}

		#endregion

		#region Context Menu Methods

		[ContextMenu("Test Shake")]
		private void TestShake() {
			StartShake();
		}

		[ContextMenu("Stop Shake")]
		private void TestStopShake() {
			StopShake();
		}

		#endregion
	}
}
