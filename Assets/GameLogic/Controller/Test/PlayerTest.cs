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
				var saves = SaveSystem.GetAllSaveInfos();
				if (saves.Count > SaveIndex) {
					GameMgr.Inst.RegisterSaveInfo(saves[SaveIndex]);
					GameMgr.Inst.LoadGame();
				} else {
					WorldGenerator.Inst.Generate();
					var saveInfo = SaveSystem.CreateSaveFile();
					GameMgr.Inst.RegisterSaveInfo(saveInfo);
					GameMgr.Inst.SaveGame();
				}
			}
		}
	}
}