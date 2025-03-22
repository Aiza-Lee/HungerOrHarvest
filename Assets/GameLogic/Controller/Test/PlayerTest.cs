using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {
			if (Input.GetKey(KeyCode.Space)) {
				Debug.Log(WorldMgr.Inst.GetAllVills[0].Coord);
			}
			if (Input.GetKeyDown(KeyCode.Q)) {
				EndGame();
			}
			if (Input.GetKeyDown(KeyCode.P)) {
				TickTrigger.Inst.Pause = !TickTrigger.Inst.Pause;
			}
			if (Input.GetKey(KeyCode.V)) {
				LogicFctry.Inst.NewVill(VillType.Normal, new(0, 0));
				Debug.Log(WorldMgr.Inst.GetAllVills.Count);
			}

			// 开始游戏
			if (Input.GetKeyDown(KeyCode.N)) {
				var saves = SaveMgr.Inst.GetSaveInfos();
				if (saves.Count > SaveIndex) {
					SaveMgr.Inst.SaveInfo = saves[SaveIndex];
					SaveMgr.Inst.LoadGame();
				} else {
					NewWorld();
				}
			}
		}

		private void NewWorld() {
			var saveInfo = SaveSystem.CreateSaveFile();
			SaveMgr.Inst.SaveInfo = saveInfo;
			WorldGenerator.Inst.Generate();
			SaveMgr.Inst.SaveGame();
		}

		private void EndGame() {
			SaveMgr.Inst.SaveGame();
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#endif
		}
	}
}