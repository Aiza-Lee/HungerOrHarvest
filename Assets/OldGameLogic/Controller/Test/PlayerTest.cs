using OldGameLogic.Model.Mgr;
using OldGameLogic.View;
using OldGameLogic.View.UI.PopUpPanels.ScreenEdgePanel;
using UnityEngine;

namespace OldGameLogic.Controller
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