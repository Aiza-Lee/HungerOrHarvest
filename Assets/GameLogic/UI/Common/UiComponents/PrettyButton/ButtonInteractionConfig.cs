using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.PrettyButton {
	/// <summary>
	/// 按钮交互配置
	/// 控制按钮的交互行为
	/// </summary>
	[System.Serializable]
	public class ButtonInteractionConfig {
		
		[Header("=== 交互时间配置 ===")]
		[SerializeField][Tooltip("启用长按检测")] private bool enableLongPress;
		[SerializeField][Tooltip("长按检测时间"), Range(0.5f, 3f)] private float _longPressTime = 1f;

		[Header("=== 状态配置 ===")]
		[SerializeField][Tooltip("是否可交互")] private bool _interactable = true;

		// 公共属性
		public float LongPressTime => _longPressTime;
		public bool EnableLongPress => enableLongPress;

		public bool Interactable {
			get => _interactable;
			set => _interactable = value;
		}
	}
}
