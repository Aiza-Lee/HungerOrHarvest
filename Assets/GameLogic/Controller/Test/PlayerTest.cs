using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Q)) {
				EndGame();
			}
			if (Input.GetKeyDown(KeyCode.P)) {
				CmdFctry.TogglePause().Run();
			}
			if (Input.GetKey(KeyCode.V)) {
				CmdFctry.CreateVill(VillType.Normal, new(0, 0)).Run();
			}

			// 开始游戏
			if (Input.GetKeyDown(KeyCode.N)) {
				var saves = SaveMgr.Inst.GetSaveInfos();
				if (saves.Count > SaveIndex) {
					CmdFctry.LoadSave(saves[SaveIndex]).Run();
				} else {
					CmdFctry.NewWrold().Run();
				}
			}
		}

		private void EndGame() {
			CmdFctry.SaveGame().Run();
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}
}