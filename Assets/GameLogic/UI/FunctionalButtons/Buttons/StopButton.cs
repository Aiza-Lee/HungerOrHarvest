using GameLogic.Features.TickSpeed;
using UnityEngine;

namespace GameLogic.UI.FunctionalButtons {
	public class StopButton : MonoBehaviour {
		public void OnClick() {
			TickSpeedAPI.SetTickPaused(true);
		}
	}
}