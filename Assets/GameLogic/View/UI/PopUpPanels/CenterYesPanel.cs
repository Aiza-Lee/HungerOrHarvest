using System;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.PopUpPanels
{
	public class CenterYesPanel : PanelBase {
		public override void OnClose() {
			OnYesChoosed = null;
		}
		public override void OnShow() {}

		[SerializeField] private TextMeshProUGUI _tipText;

		public void SetTipText(string tipText) {
			_tipText.text = tipText;
		}
		// 外部传入的Callback
		public event Action OnYesChoosed;

		public void OnYesClicked() {
			OnYesChoosed?.Invoke();
			this.Toggle();
		}
	}
}