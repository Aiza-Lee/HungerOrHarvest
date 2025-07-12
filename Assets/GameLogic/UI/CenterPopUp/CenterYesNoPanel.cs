using System;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.UI.Common.CenterPopUp {
	public class CenterYesNoPanel : PanelBase, IRegisterUiMgr {
		public override void OnClose() {
			OnYesChoosed = null;
			OnNoChoosed = null;
		}
		public override void OnShow() { }

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