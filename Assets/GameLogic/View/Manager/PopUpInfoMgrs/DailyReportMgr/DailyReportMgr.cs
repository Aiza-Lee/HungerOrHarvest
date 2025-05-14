using GameLogic.View.UI.PopUpPanels.ScreenEdgePanel;
using NSFrame;

namespace GameLogic.View.Mgr.PopUpInfoMgrs
{
	public class DailyReportMgr : MonoSingleton<DailyReportMgr> {
		private void Start() {
			EventSystem.AddListener((int)ModelEvt.NightStart_0, OnNightStart, EventType.Model);
		}

		private void OnNightStart() {
			var panel = UIMgr.Inst.TogglePanel<DailyReportPanel>();
			var tipText = $"每日总结\n";
			panel.SetTipText(tipText);
		}
	}
}