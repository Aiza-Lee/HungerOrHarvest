using GameLogic.Features.WorldDataManager;
using GameLogic.UI.Common.CenterPopUp;
using GameLogic.UI.Common.UiMgr;
using UnityEngine;

namespace GameLogic.UI.StartMenu {
	public class NewWorldButton : MonoBehaviour {
		public void OnClicked() {
			var popup = UIMgr.Inst.TogglePanel<CenterStrReceivePanel>()
				.SetTipText("请输入新世界的名字")
				.SetHintStr("输入...")
				.SetInputText("");
			popup.OnYesChoosed += (name) => {
				name = name.Trim().Replace("\u200B", "");
				var check = UIMgr.Inst.FindPanel<SelectSavePanel>().CheckNameValid(name);
				if (!check.Item1) {
					UIMgr.Inst.TogglePanel<CenterYesPanel>().SetTipText($"无效的世界名称：{check.Item2}");
					return;
				}
				WorldDataManagerAPI.NewRandomWorld(name);
				UIMgr.Inst.TogglePanel<SelectSavePanel>();
			};
		}
	}
}