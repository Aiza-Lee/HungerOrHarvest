using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.ClapBoard
{
	/// <summary>
	/// 遮挡板，用于遮挡某个UI（这里注册的_attachedPanel）没有覆盖的到的地方
	/// <para> 并定义点击到没有遮挡的区域时的行为（什么都不做或者关闭_attachedPanel）</para>
	/// </summary>
	public class ClapBoard : MonoBehaviour {
		public enum ClapBoardType {
			TogglePanel,
			DoNothing,
		}
		[SerializeField] private ClapBoardType _type;
		[SerializeField] private PanelBase _attachedPanel;

		public void OnClicked() {
			if (_type == ClapBoardType.DoNothing) return;
			_attachedPanel.Toggle();
		}
	}
}