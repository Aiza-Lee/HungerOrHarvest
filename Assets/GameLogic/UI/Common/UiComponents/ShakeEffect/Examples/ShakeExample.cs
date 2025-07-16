using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.ShakeEffect {
	/// <summary>
	/// 独立抖动效果使用示例
	/// </summary>
	public class ShakeExample : MonoBehaviour {
		[Header("Shake Components")]
		public ShakeEffect positionShake;
		public ShakeEffect scaleShake;
		public ShakeEffect rotationShake;

		[Header("Basic Test Parameters")]
		[SerializeField] private float testPositionDuration = 1f;
		[SerializeField] private float testPositionMagnitude = 2f;
		[SerializeField] private float testPositionFrequency = 15f;
		
		[SerializeField] private float testScaleDuration = 1f;
		[SerializeField] private float testScaleMagnitude = 0.1f;
		[SerializeField] private float testScaleFrequency = 20f;
		
		[SerializeField] private float testRotationDuration = 1f;
		[SerializeField] private float testRotationMagnitude = 5f;
		[SerializeField] private float testRotationFrequency = 10f;

		[Header("Damage Effect Parameters")]
		[SerializeField] private float damageShakeDuration = 0.3f;
		[SerializeField] private float damageShakeMagnitude = 0.5f;
		[SerializeField] private float damageShakeFrequency = 30f;
		[SerializeField] private float damageScaleMagnitude = 0.05f;
		[SerializeField] private float damageScaleFrequency = 25f;

		[Header("Earthquake Effect Parameters")]
		[SerializeField] private float earthquakeShakeDuration = 3f;
		[SerializeField] private float earthquakeShakeMagnitude = 0.2f;
		[SerializeField] private float earthquakeShakeFrequency = 8f;

		[Header("Explosion Effect Parameters")]
		[SerializeField] private float explosionPositionDuration = 0.5f;
		[SerializeField] private float explosionPositionMagnitude = 1f;
		[SerializeField] private float explosionPositionFrequency = 40f;
		[SerializeField] private float explosionScaleDuration = 0.4f;
		[SerializeField] private float explosionScaleMagnitude = 0.2f;
		[SerializeField] private float explosionScaleFrequency = 35f;
		[SerializeField] private float explosionRotationDuration = 0.3f;
		[SerializeField] private float explosionRotationMagnitude = 10f;
		[SerializeField] private float explosionRotationFrequency = 30f;

		[Header("Hit Effect Parameters")]
		[SerializeField] private float hitShakeDuration = 0.2f;
		[SerializeField] private float hitShakeMagnitude = 0.3f;
		[SerializeField] private float hitShakeFrequency = 50f;

		[Header("Nervous Effect Parameters")]
		[SerializeField] private float nervousShakeDuration = 2f;
		[SerializeField] private float nervousShakeMagnitude = 2f;
		[SerializeField] private float nervousShakeFrequency = 12f;

		[Header("Debug UI Settings")]
		[SerializeField] private bool showDebugUI = true;
		[SerializeField] private bool showParameters = false;
		[SerializeField] private int selectedEffectTab = 0; // 0: Basic, 1: Damage, 2: Earthquake, 3: Explosion, 4: Hit, 5: Nervous
		[SerializeField] private float uiScale = 1f; // UI缩放比例，用于适配不同分辨率

		private void Start() {
			// 自动获取或添加 ShakeEffect 组件
			if (positionShake == null) {
				positionShake = gameObject.AddComponent<ShakeEffect>();
				positionShake.SetShakeType(ShakeEffect.ShakeType.LocalPosition);
				positionShake.SetShakeAxis(ShakeEffect.ShakeAxis.XY);
			}

			if (scaleShake == null) {
				scaleShake = gameObject.AddComponent<ShakeEffect>();
				scaleShake.SetShakeType(ShakeEffect.ShakeType.Scale);
				scaleShake.SetShakeAxis(ShakeEffect.ShakeAxis.XYZ);
			}

			if (rotationShake == null) {
				rotationShake = gameObject.AddComponent<ShakeEffect>();
				rotationShake.SetShakeType(ShakeEffect.ShakeType.Rotation);
				rotationShake.SetShakeAxis(ShakeEffect.ShakeAxis.Z);
			}

			// 根据屏幕分辨率自动调整UI缩放
			CalculateUIScale();
		}

		private void CalculateUIScale() {
			// 基于屏幕DPI和分辨率计算合适的缩放比例
			float referenceDPI = 96f; // 标准DPI
			float currentDPI = Screen.dpi > 0 ? Screen.dpi : referenceDPI;
			
			// 计算基于DPI的缩放
			float dpiScale = currentDPI / referenceDPI;
			
			// 限制缩放范围，避免过大或过小
			uiScale = Mathf.Clamp(dpiScale, 0.8f, 2.5f);
			
			// 对于非常高分辨率的屏幕，适当调整
			if (Screen.width > 2560 || Screen.height > 1440) {
				uiScale *= 1.2f;
			}
		}

		[ContextMenu("Test Position Shake")]
		public void TestPositionShake() {
			if (positionShake != null) {
				positionShake.StartShake(testPositionDuration, testPositionMagnitude, testPositionFrequency);
			}
		}

		[ContextMenu("Test Scale Shake")]
		public void TestScaleShake() {
			if (scaleShake != null) {
				scaleShake.StartShake(testScaleDuration, testScaleMagnitude, testScaleFrequency);
			}
		}

		[ContextMenu("Test Rotation Shake")]
		public void TestRotationShake() {
			if (rotationShake != null) {
				rotationShake.StartShake(testRotationDuration, testRotationMagnitude, testRotationFrequency);
			}
		}

		[ContextMenu("Stop All Shakes")]
		public void StopAllShakes() {
			if (positionShake != null) {
				positionShake.StopShake();
			}
			if (scaleShake != null) {
				scaleShake.StopShake();
			}
			if (rotationShake != null) {
				rotationShake.StopShake();
			}
		}

		[ContextMenu("Damage Effect")]
		public void DamageEffect() {
			// 受伤效果：快速强烈位置抖动 + 轻微缩放抖动
			if (positionShake != null) {
				positionShake
					.StartShake(damageShakeDuration, damageShakeMagnitude, damageShakeFrequency)
					.SetOnComplete(() => Debug.Log("Damage shake completed!"));
			}

			if (scaleShake != null) {
				scaleShake.StartShake(damageShakeDuration, damageScaleMagnitude, damageScaleFrequency);
			}
		}

		[ContextMenu("Earthquake Effect")]
		public void EarthquakeEffect() {
			// 地震效果：持续缓慢位置抖动
			if (positionShake != null) {
				positionShake
					.SetShakeAxis(ShakeEffect.ShakeAxis.XY)
					.StartShake(earthquakeShakeDuration, earthquakeShakeMagnitude, earthquakeShakeFrequency)
					.SetOnUpdate(offset => {
						// 可以在这里添加音效或其他效果
						if (offset.magnitude > 0.1f) {
							// Debug.Log($"Strong shake: {offset.magnitude}");
						}
					});
			}
		}

		[ContextMenu("Explosion Effect")]
		public void ExplosionEffect() {
			// 爆炸效果：瞬间强烈多轴抖动
			if (positionShake != null) {
				positionShake
					.SetShakeAxis(ShakeEffect.ShakeAxis.XYZ)
					.StartShake(explosionPositionDuration, explosionPositionMagnitude, explosionPositionFrequency);
			}

			if (scaleShake != null) {
				scaleShake.StartShake(explosionScaleDuration, explosionScaleMagnitude, explosionScaleFrequency);
			}

			if (rotationShake != null) {
				rotationShake.StartShake(explosionRotationDuration, explosionRotationMagnitude, explosionRotationFrequency);
			}
		}

		[ContextMenu("Hit Effect")]
		public void HitEffect() {
			// 击中效果：快速X轴位置抖动
			if (positionShake != null) {
				positionShake
					.SetShakeAxis(ShakeEffect.ShakeAxis.X)
					.StartShake(hitShakeDuration, hitShakeMagnitude, hitShakeFrequency);
			}
		}

		[ContextMenu("Nervous Effect")]
		public void NervousEffect() {
			// 紧张效果：轻微持续旋转抖动
			if (rotationShake != null) {
				rotationShake
					.SetShakeAxis(ShakeEffect.ShakeAxis.Z)
					.StartShake(nervousShakeDuration, nervousShakeMagnitude, nervousShakeFrequency);
			}
		}

		private void Update() {
			// 键盘快捷键测试
			if (Input.GetKeyDown(KeyCode.Space)) {
				TestPositionShake();
			}

			if (Input.GetKeyDown(KeyCode.Q)) {
				TestScaleShake();
			}

			if (Input.GetKeyDown(KeyCode.E)) {
				TestRotationShake();
			}

			if (Input.GetKeyDown(KeyCode.X)) {
				DamageEffect();
			}

			if (Input.GetKeyDown(KeyCode.Z)) {
				EarthquakeEffect();
			}

			if (Input.GetKeyDown(KeyCode.C)) {
				ExplosionEffect();
			}

			if (Input.GetKeyDown(KeyCode.V)) {
				HitEffect();
			}

			if (Input.GetKeyDown(KeyCode.B)) {
				NervousEffect();
			}

			if (Input.GetKeyDown(KeyCode.S)) {
				StopAllShakes();
			}
		}

		private void OnGUI() {
			if (!showDebugUI) return;

			// 主控制面板
			GUILayout.BeginArea(new Rect(10, 10, 320, 280));
			GUILayout.Box("Shake Effect Test Controls", GUILayout.Width(310));
			
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Position (Space)", GUILayout.Width(100))) TestPositionShake();
			if (GUILayout.Button("Scale (Q)", GUILayout.Width(80))) TestScaleShake();
			if (GUILayout.Button("Rotation (E)", GUILayout.Width(90))) TestRotationShake();
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Damage (X)", GUILayout.Width(90))) DamageEffect();
			if (GUILayout.Button("Earthquake (Z)", GUILayout.Width(110))) EarthquakeEffect();
			if (GUILayout.Button("Explosion (C)", GUILayout.Width(100))) ExplosionEffect();
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Hit (V)", GUILayout.Width(70))) HitEffect();
			if (GUILayout.Button("Nervous (B)", GUILayout.Width(90))) NervousEffect();
			if (GUILayout.Button("Stop All (S)", GUILayout.Width(90))) StopAllShakes();
			GUILayout.EndHorizontal();

			GUILayout.Space(10);
			showParameters = GUILayout.Toggle(showParameters, "Show Parameters", GUILayout.Width(150));
			
			GUILayout.EndArea();

			// 参数调试面板
			if (showParameters) {
				GUILayout.BeginArea(new Rect(340, 10, 450, 700));
				GUILayout.Box("Effect Parameters", GUILayout.Width(440));
				
				// 效果选择标签页
				GUILayout.BeginHorizontal();
				if (GUILayout.Toggle(selectedEffectTab == 0, "Basic", "Button", GUILayout.Width(60))) selectedEffectTab = 0;
				if (GUILayout.Toggle(selectedEffectTab == 1, "Damage", "Button", GUILayout.Width(60))) selectedEffectTab = 1;
				if (GUILayout.Toggle(selectedEffectTab == 2, "Earthquake", "Button", GUILayout.Width(80))) selectedEffectTab = 2;
				if (GUILayout.Toggle(selectedEffectTab == 3, "Explosion", "Button", GUILayout.Width(70))) selectedEffectTab = 3;
				if (GUILayout.Toggle(selectedEffectTab == 4, "Hit", "Button", GUILayout.Width(40))) selectedEffectTab = 4;
				if (GUILayout.Toggle(selectedEffectTab == 5, "Nervous", "Button", GUILayout.Width(60))) selectedEffectTab = 5;
				GUILayout.EndHorizontal();
				
				GUILayout.Space(10);

				// 根据选择的标签页显示对应参数
				switch (selectedEffectTab) {
					case 0: // Basic Test Parameters
						DrawBasicTestParameters();
						break;
					case 1: // Damage Effect
						DrawDamageEffectParameters();
						break;
					case 2: // Earthquake Effect
						DrawEarthquakeEffectParameters();
						break;
					case 3: // Explosion Effect
						DrawExplosionEffectParameters();
						break;
					case 4: // Hit Effect
						DrawHitEffectParameters();
						break;
					case 5: // Nervous Effect
						DrawNervousEffectParameters();
						break;
				}

				GUILayout.EndArea();
			}
		}

		private void DrawBasicTestParameters() {
			GUILayout.Label("Position Shake Test:", GUI.skin.box);
			testPositionDuration = FloatSlider("Duration", testPositionDuration, 0.1f, 5f);
			testPositionMagnitude = FloatSlider("Magnitude", testPositionMagnitude, 0.1f, 10f);
			testPositionFrequency = FloatSlider("Frequency", testPositionFrequency, 1f, 50f);
			
			GUILayout.Space(10);
			GUILayout.Label("Scale Shake Test:", GUI.skin.box);
			testScaleDuration = FloatSlider("Duration", testScaleDuration, 0.1f, 5f);
			testScaleMagnitude = FloatSlider("Magnitude", testScaleMagnitude, 0.01f, 1f);
			testScaleFrequency = FloatSlider("Frequency", testScaleFrequency, 1f, 50f);
			
			GUILayout.Space(10);
			GUILayout.Label("Rotation Shake Test:", GUI.skin.box);
			testRotationDuration = FloatSlider("Duration", testRotationDuration, 0.1f, 5f);
			testRotationMagnitude = FloatSlider("Magnitude", testRotationMagnitude, 1f, 50f);
			testRotationFrequency = FloatSlider("Frequency", testRotationFrequency, 1f, 50f);
		}

		private void DrawDamageEffectParameters() {
			GUILayout.Label("Damage Effect - Position Shake:", GUI.skin.box);
			damageShakeDuration = FloatSlider("Duration", damageShakeDuration, 0.1f, 2f);
			damageShakeMagnitude = FloatSlider("Magnitude", damageShakeMagnitude, 0.1f, 5f);
			damageShakeFrequency = FloatSlider("Frequency", damageShakeFrequency, 10f, 60f);
			
			GUILayout.Space(10);
			GUILayout.Label("Damage Effect - Scale Shake:", GUI.skin.box);
			damageScaleMagnitude = FloatSlider("Magnitude", damageScaleMagnitude, 0.01f, 0.5f);
			damageScaleFrequency = FloatSlider("Frequency", damageScaleFrequency, 10f, 60f);
			
			GUILayout.Space(10);
			if (GUILayout.Button("Test Damage Effect", GUILayout.Height(30))) {
				DamageEffect();
			}
		}

		private void DrawEarthquakeEffectParameters() {
			GUILayout.Label("Earthquake Effect - Position Shake:", GUI.skin.box);
			earthquakeShakeDuration = FloatSlider("Duration", earthquakeShakeDuration, 1f, 10f);
			earthquakeShakeMagnitude = FloatSlider("Magnitude", earthquakeShakeMagnitude, 0.05f, 1f);
			earthquakeShakeFrequency = FloatSlider("Frequency", earthquakeShakeFrequency, 2f, 20f);
			
			GUILayout.Space(10);
			if (GUILayout.Button("Test Earthquake Effect", GUILayout.Height(30))) {
				EarthquakeEffect();
			}
		}

		private void DrawExplosionEffectParameters() {
			GUILayout.Label("Explosion Effect - Position Shake:", GUI.skin.box);
			explosionPositionDuration = FloatSlider("Duration", explosionPositionDuration, 0.1f, 2f);
			explosionPositionMagnitude = FloatSlider("Magnitude", explosionPositionMagnitude, 0.1f, 5f);
			explosionPositionFrequency = FloatSlider("Frequency", explosionPositionFrequency, 10f, 80f);
			
			GUILayout.Space(10);
			GUILayout.Label("Explosion Effect - Scale Shake:", GUI.skin.box);
			explosionScaleDuration = FloatSlider("Duration", explosionScaleDuration, 0.1f, 2f);
			explosionScaleMagnitude = FloatSlider("Magnitude", explosionScaleMagnitude, 0.01f, 1f);
			explosionScaleFrequency = FloatSlider("Frequency", explosionScaleFrequency, 10f, 80f);
			
			GUILayout.Space(10);
			GUILayout.Label("Explosion Effect - Rotation Shake:", GUI.skin.box);
			explosionRotationDuration = FloatSlider("Duration", explosionRotationDuration, 0.1f, 2f);
			explosionRotationMagnitude = FloatSlider("Magnitude", explosionRotationMagnitude, 1f, 50f);
			explosionRotationFrequency = FloatSlider("Frequency", explosionRotationFrequency, 10f, 80f);
			
			GUILayout.Space(10);
			if (GUILayout.Button("Test Explosion Effect", GUILayout.Height(30))) {
				ExplosionEffect();
			}
		}

		private void DrawHitEffectParameters() {
			GUILayout.Label("Hit Effect - Position Shake (X-Axis):", GUI.skin.box);
			hitShakeDuration = FloatSlider("Duration", hitShakeDuration, 0.1f, 1f);
			hitShakeMagnitude = FloatSlider("Magnitude", hitShakeMagnitude, 0.1f, 2f);
			hitShakeFrequency = FloatSlider("Frequency", hitShakeFrequency, 20f, 80f);
			
			GUILayout.Space(10);
			if (GUILayout.Button("Test Hit Effect", GUILayout.Height(30))) {
				HitEffect();
			}
		}

		private void DrawNervousEffectParameters() {
			GUILayout.Label("Nervous Effect - Rotation Shake (Z-Axis):", GUI.skin.box);
			nervousShakeDuration = FloatSlider("Duration", nervousShakeDuration, 0.5f, 5f);
			nervousShakeMagnitude = FloatSlider("Magnitude", nervousShakeMagnitude, 0.5f, 10f);
			nervousShakeFrequency = FloatSlider("Frequency", nervousShakeFrequency, 5f, 30f);
			
			GUILayout.Space(10);
			if (GUILayout.Button("Test Nervous Effect", GUILayout.Height(30))) {
				NervousEffect();
			}
		}

		private float FloatSlider(string label, float value, float min, float max) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(label, GUILayout.Width(120));
			value = GUILayout.HorizontalSlider(value, min, max, GUILayout.Width(150));
			GUILayout.Label(value.ToString("F2"), GUILayout.Width(40));
			GUILayout.EndHorizontal();
			return value;
		}
	}
}
