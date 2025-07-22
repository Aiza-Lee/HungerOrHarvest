using GameLogic.UI.Common.UiMgr;
using UnityEngine;

namespace GameLogic.UI.FunctionalButtons {
	public class SettingsButton : MonoBehaviour {
		public void OnClick() {
			UIMgr.Inst.ShowPanel<SettingMenu.MainPanel>();
		}
	}
}