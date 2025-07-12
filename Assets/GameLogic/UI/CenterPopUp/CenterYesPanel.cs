using System;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.UI.Common.CenterPopUp {
	public class CenterYesPanel : PanelBase, IRegisterUiMgr {
		public override void OnClose() {
			OnYesChoosed = null;
		}
		public override void OnShow() { }

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