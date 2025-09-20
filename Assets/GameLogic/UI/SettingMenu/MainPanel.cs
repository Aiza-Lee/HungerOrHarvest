using GameLogic.Features.WorldDataManager;
using GameLogic.UI.Common.UiMgr;
using NSFrame;

namespace GameLogic.UI.SettingMenu {
	public class MainPanel : PanelBase, IRegisterUiMgr {
		public override void OnClose() {
		}

		public override void OnShow() {
		}

		public void BackToStartMenu() {
			WorldDataManagerAPI.ClearWorld();
			UIMgr.Inst.ClosePanel<MainPanel>();
			UIMgr.Inst.ClosePanel<WorldRepo.MainPanel>();
			UIMgr.Inst.ClosePanel<WorldVill.MainPanel>();
			UIMgr.Inst.ClosePanel<FunctionalButtons.MainPanel>();
			UIMgr.Inst.ShowPanel<StartMenu.MainPanel>();
		}
	}
}