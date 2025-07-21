using GameLogic.Common.View;
using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.PrettyButton {
	/// <summary>
	/// 按钮主题配置 - ScriptableObject
	/// 提供统一的按钮外观和行为配置
	/// </summary>
	[CreateAssetMenu(fileName = "ButtonTheme", menuName = "UI/Button Theme Config", order = 1)]
	public class ButtonThemeConfig : ScriptableObject {
		
		[Header("=== 颜色配置 ===")]
		[SerializeField][Tooltip("启用颜色变化")] private bool _enableColor = true;
		[SerializeField][Tooltip("悬停状态颜色")] private Color _hoverColor = new(0.9f, 0.9f, 0.9f, 1f);
		[SerializeField][Tooltip("按下状态颜色")] private Color _pressedColor = new(0.7f, 0.7f, 0.7f, 1f);
		[SerializeField][Tooltip("禁用状态颜色")] private Color _disabledColor = new(0.6f, 0.6f, 0.6f, 1f);
		[SerializeField][Tooltip("颜色变化参数")] private ChangeInfo _colorChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };

		[Header("=== 缩放配置 ===")]
		[SerializeField][Tooltip("启用缩放变化")] private bool _enableScale = true;
		[SerializeField][Tooltip("悬停时的缩放倍数"), Range(0.8f, 1.5f)] private float _hoverScale = 1.05f;
		[SerializeField][Tooltip("悬停时的缩放变化参数")] private ChangeInfo _hoverScaleChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };
		[SerializeField][Tooltip("取消悬停时的缩放变化参数")] private ChangeInfo _unHoverScaleChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };
		[SerializeField][Tooltip("按下时的缩放倍数"), Range(0.5f, 1.2f)] private float _pressedScale = 0.95f;
		[SerializeField][Tooltip("按下时的缩放变化参数")] private ChangeInfo _pressedScaleChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };
		[SerializeField][Tooltip("取消按下时的缩放变化参数")] private ChangeInfo _unPressedScaleChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };

		[Header("=== 按下后偏移配置 ===")]
		[SerializeField][Tooltip("启用按下后偏移")] private bool _enablePressedOffset = true;
		[SerializeField][Tooltip("按下时的偏移距离")] private Vector2 _pressedOffset = Vector2.down * 2f;
		[SerializeField][Tooltip("按下时的偏移变化参数")] private ChangeInfo _pressedOffsetChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };
		[SerializeField][Tooltip("取消按下时的偏移变化参数")] private ChangeInfo _unPressedOffsetChangeInfo = new() { CurveType = ChangeCurveType.CubicOut, TotalTime = 0.1f, UseLogicTime = false };

		[Header("=== 粒子效果 ===")]
		[SerializeField][Tooltip("启用点击粒子效果")] private bool _enableClickParticles = false;
		[SerializeField][Tooltip("粒子预制体")] private ParticleSystem _clickParticlePrefab;

		[Header("=== 音效配置 ===")]
		[SerializeField][Tooltip("启用音效")] private bool _enableAudio = true;
		[SerializeField][Tooltip("悬停音效")] private AudioClip _hoverSound;
		[SerializeField][Tooltip("点击音效")] private AudioClip _clickSound;
		[SerializeField][Tooltip("音量"), Range(0f, 1f)] private float _volume = 0.5f;

		public Color HoverColor => _hoverColor;
		public Color PressedColor => _pressedColor;
		public Color DisabledColor => _disabledColor;

		public Vector2 HoverScale => Vector2.one * _hoverScale;
		public Vector2 PressedScale => Vector2.one * _pressedScale;
		public Vector2 PressedOffset => _pressedOffset;
		
		public bool EnableClickParticles => _enableClickParticles;
		public bool EnableColor => _enableColor;
		public bool EnableScale => _enableScale;
		public bool EnablePressedOffset => _enablePressedOffset;
		public bool EnableAudio => _enableAudio;


		public ChangeInfo ColorChangeInfo => _colorChangeInfo;
		public ChangeInfo HoverScaleChangeInfo => _hoverScaleChangeInfo;
		public ChangeInfo UnhoverScaleChangeInfo => _unHoverScaleChangeInfo;
		public ChangeInfo PressedScaleChangeInfo => _pressedScaleChangeInfo;
		public ChangeInfo UnpressedScaleChangeInfo => _unPressedScaleChangeInfo;
		public ChangeInfo PressedOffsetChangeInfo => _pressedOffsetChangeInfo;
		public ChangeInfo UnpressedOffesetChangeInfo => _unPressedOffsetChangeInfo;

		public ParticleSystem ClickParticlePrefab => _clickParticlePrefab;
		public AudioClip HoverSound => _hoverSound;
		public AudioClip ClickSound => _clickSound;
		public float Volume => _volume;

	}
}
