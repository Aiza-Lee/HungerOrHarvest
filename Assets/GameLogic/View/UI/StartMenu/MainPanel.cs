using GameLogic.Controller;
using NSFrame;

namespace GameLogic.View.UI.StartMenu
{
	public class MainPanel : PanelBase {
		public override void OnClose() {}
		public override void OnShow() {}


		#region PublicMethods
		public void OnEnterClicked() {
			if (GameModelMgr.Inst.SaveInfoSeted() && GameViewMgr.Inst.SaveInfoSeted()) {
				UIMgr.Inst.TogglePanel<WorldRepoPanel.MainPanel>();
				UIMgr.Inst.TogglePanel<WorldVillPanel.MainPanel>();
				UIMgr.Inst.TogglePanel<MainPanel>();
				PlayerControllMgr.Inst.SetWorldMainControll(true);
				CmdRunner.Run("/pause");
			} else {
				var popup = UIMgr.Inst.TogglePanel<PopUpPanels.CenterYesPanel>();
				popup.SetTipText("请先选择存档");
			}
		}
		public void OnSelectSaveClicked() {
			UIMgr.Inst.TogglePanel<SelectSavePanel.SelectSavePanel>();
		}
		public void OnSettingClicked() {}
		public void OnExitClicked() {
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
		#endregion
	}
}