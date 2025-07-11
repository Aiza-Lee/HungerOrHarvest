using GameLogic.Features.NewWorldCreator;
using GameLogic.Features.SpeedControl;
using UnityEngine;

namespace GameLogic.Test {
	public class MainTest : MonoBehaviour {
		private void Update() {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				SpeedControlAPI.SetSpeedControlInputEnabled(true);
				Debug.Log("Speed control input enabled.");
			}
			if (Input.GetKeyDown(KeyCode.N)) {
				// 创建一个新的随机世界
				NewWorldAPI.NewRandomWorld("TestWorld");
				Debug.Log("New random world created.");
			}
		}
	}
}