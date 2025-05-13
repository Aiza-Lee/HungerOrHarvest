using System;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.PopUpPanels
{
	public class CenterStrReceivePanel : PanelBase {
		public override void OnClose() {
			OnYesChoosed = null;
			_inputText.text = "";
		}
		public override void OnShow() {}

		[SerializeField] private TextMeshProUGUI _tipText;
		[SerializeField] private TextMeshProUGUI _inputText;
		[SerializeField] private TextMeshProUGUI _hintText;

		// 外部传入的Callback
		public event Action<string> OnYesChoosed;

		public void SetTipText(string tipText) {
			_tipText.text = tipText;
		}
		public void SetHintStr(string hint) {
			_hintText.text = hint;
		}

		public void OnYesClicked() {
			OnYesChoosed?.Invoke(_inputText.text);
			this.Toggle();
		}
		public void OnNoClicked() {
			this.Toggle();
		}
	}
}