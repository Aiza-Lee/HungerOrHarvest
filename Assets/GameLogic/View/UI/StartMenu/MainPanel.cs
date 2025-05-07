using NSFrame;

namespace GameLogic.View.UI.StartMenu
{
	public class MainPanel : PanelBase {
		public override void OnClose() {}
		public override void OnShow() {}


		#region PublicMethods
		public void OnEnterClicked() {}
		public void OnSelectSaveClicked() {}
		public void OnSettingClicked() {}
		public void OnExitClicked() {
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
		#endregion
	}
}