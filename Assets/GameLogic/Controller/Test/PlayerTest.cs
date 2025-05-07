using GameLogic.View;
using NSFrame;
using UnityEngine;

namespace GameLogic.Controller
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {

			// 开始游戏
			if (Input.GetKeyDown(KeyCode.F12)) {
				var saves = SaveSystem.GetAllSaveInfos();
				if (saves.Count > SaveIndex) {
					GameModelMgr.Inst.SetSaveInfo(saves[SaveIndex]);
					GameViewMgr.Inst.SetSaveInfo(saves[SaveIndex]);
					CmdRunner.Run("/load");
				} else {
					CmdRunner.Run("/world-new");
				}
			}

			// PassNight
			if (Input.GetKeyDown(KeyCode.F10)) {
				LogicTimeMgr.Inst.PassNight();
			}
		}
	}
}