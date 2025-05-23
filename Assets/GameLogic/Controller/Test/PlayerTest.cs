using GameLogic.Model.Mgr;
using GameLogic.View;
using GameLogic.View.UI.PopUpPanels.ScreenEdgePanel;
using UnityEngine;

namespace GameLogic.Controller
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {

			// PassNight
			if (Input.GetKeyDown(KeyCode.F10)) {
				LogicTimeMgr.Inst.PassNight();
				UIMgr.Inst.FindPanel<DailyReportPanel>().ClosePanel();
			}
		}
	}
}