using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.ClapBoard
{
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