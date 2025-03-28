using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.F11)) {
				CmdRunner.Run("/save");
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
				#endif
			}

			// 开始游戏
			if (Input.GetKeyDown(KeyCode.F12)) {
				var saves = SaveMgr.Inst.GetSaveInfos();
				if (saves.Count > SaveIndex) {
					SaveMgr.Inst.SaveInfo = saves[SaveIndex];
					SaveMgr.Inst.LoadGame();
				} else {
					WorldGenerator.Inst.Generate();
					var saveInfo = SaveSystem.CreateSaveFile();
					SaveMgr.Inst.SaveInfo = saveInfo;
					SaveMgr.Inst.SaveGame();
				}
			}
		}
	}
}