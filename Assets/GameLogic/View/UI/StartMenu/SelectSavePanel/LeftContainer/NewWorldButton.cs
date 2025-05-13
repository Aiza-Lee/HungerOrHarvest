using GameLogic.Controller;
using GameLogic.View.UI.PopUpPanels;
using UnityEngine;

namespace GameLogic.View.UI.StartMenu.SelectSavePanel
{
	public class NewWorldButton : MonoBehaviour {
		public void OnClicked() {
			var popup = UIMgr.Inst.TogglePanel<CenterStrReceivePanel>();
			popup.SetTipText("请输入新世界的名字");
			popup.SetHintStr("输入...");
			popup.OnYesChoosed += (name) => {
				name = name.Trim().Replace("\u200B", "");
				WorldGenerator.Inst.GenerateRandomWorld(name);
				UIMgr.Inst.FindPanel<SelectSavePanel>().Refresh();
			};
		}
	}
}