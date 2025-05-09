using System;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.PopUpPanels
{
	public class CenterYesNoPanel : PanelBase {
		public override void OnClose() {
			OnYesChoosed = null;
			OnNoChoosed = null;
		}
		public override void OnShow() {}

		[SerializeField] private TextMeshProUGUI _tipText;

		public void SetTipText(string tipText) {
			_tipText.text = tipText;
		}
		// 外部传入的Callback
		public event Action OnYesChoosed;
		// 外部传入的Callback
		public event Action OnNoChoosed;

		public void OnYesClicked() {
			OnYesChoosed?.Invoke();
			this.Toggle();
		}
		public void OnNoClicked() {
			OnNoChoosed?.Invoke();
			this.Toggle();
		}
	}
}