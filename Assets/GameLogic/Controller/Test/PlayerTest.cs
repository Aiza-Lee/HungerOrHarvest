using GameLogic.View;
using NSFrame;
using UnityEngine;

namespace GameLogic.Controller
{
	public class PlayerTest : MonoBehaviour {

		public int SaveIndex;

		private void Update() {

			// PassNight
			if (Input.GetKeyDown(KeyCode.F10)) {
				LogicTimeMgr.Inst.PassNight();
			}
		}
	}
}