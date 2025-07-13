using System;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.UI.Common.CenterPopUp {
	public class CenterStrReceivePanel : PanelBase, IRegisterUiMgr {
	public override void OnClose() {
		OnYesChoosed = null;
		if (_inputText != null) {
			_inputText.text = "";
		}
	}
		public override void OnShow() { }

	[SerializeField] private TextMeshProUGUI _tipText;
	[SerializeField] private TMP_InputField _inputText;
	[SerializeField] private TextMeshProUGUI _hintText;

		// 外部传入的Callback
		public event Action<string> OnYesChoosed;

	public CenterStrReceivePanel SetTipText(string tipText) { 
		if (_tipText != null) _tipText.text = tipText; 
		return this; 
	}
	public CenterStrReceivePanel SetHintStr(string hint) { 
		if (_hintText != null) _hintText.text = hint; 
		return this; 
	}
	public CenterStrReceivePanel SetInputText(string input) { 
		if (_inputText != null) _inputText.text = input; 
		return this; 
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