using GameLogic.Features.WorldDataManager;
using GameLogic.UI.Common.CenterPopUp;
using GameLogic.UI.Common.UiMgr;
using NSFrame;

namespace GameLogic.UI.StartMenu {
	public class MainPanel : PanelBase, IRegisterUiMgr {
		public override void OnClose() {}
		public override void OnShow() {}


		#region PublicMethods
		public void OnEnterClicked() {
			if (WorldDataManagerAPI.IsWorldLoaded()) {
				UIMgr.Inst.TogglePanel<WorldRepo.MainPanel>();
				UIMgr.Inst.TogglePanel<WorldVill.MainPanel>();
				UIMgr.Inst.TogglePanel<FunctionalButtons.MainPanel>();
				UIMgr.Inst.TogglePanel<MainPanel>();

			} else {
				var popup = UIMgr.Inst.TogglePanel<CenterYesPanel>();
				popup.SetTipText("请先选择存档");
			}
		}
		public void OnSelectSaveClicked() {
			UIMgr.Inst.TogglePanel<SelectSavePanel>();
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