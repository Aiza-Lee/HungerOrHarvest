using GameLogic.Features.WorldDataManager;
using GameLogic.UI.Common.CenterPopUp;
using GameLogic.UI.Common.UiMgr;
using UnityEngine;

namespace GameLogic.UI.StartMenu {
	public class NewWorldButton : MonoBehaviour {
		public void OnClicked() {
			var popup = UIMgr.Inst.TogglePanel<CenterStrReceivePanel>();
			popup.SetTipText("请输入新世界的名字");
			popup.SetHintStr("输入...");
			popup.OnYesChoosed += (name) => {
				name = name.Trim().Replace("\u200B", "");
				WorldDataManagerAPI.NewRandomWorld(name);
			};
		}
		private void Update() {
			if (/* todo: ditry check true*/true) {
				UIMgr.Inst.FindPanel<SelectSavePanel>().Refresh();
			}
		}
	}
}