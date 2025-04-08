using GameLogic.View;
using NSFrame;
using UnityEngine;

namespace GameLogic.Controller
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {
			// 保存并退出游戏
			if (Input.GetKeyDown(KeyCode.F11)) {
				CmdRunner.Run("/save");
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
				#endif
			}

			// 开始游戏
			if (Input.GetKeyDown(KeyCode.F12)) {
				var saves = SaveSystem.GetAllSaveInfos();
				if (saves.Count > SaveIndex) {
					GameModelMgr.Inst.RegisterSaveInfo(saves[SaveIndex]);
					GameViewMgr.Inst.RegisterSaveInfo(saves[SaveIndex]);
					CmdRunner.Run("/load");
				} else {
					CmdRunner.Run("/world-new");
				}
			}
		}
	}
}