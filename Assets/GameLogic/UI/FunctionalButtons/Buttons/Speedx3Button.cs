using GameLogic.Features.TickSpeed;
using UnityEngine;

namespace GameLogic.UI.FunctionalButtons {
	public class Speedx3Button : MonoBehaviour {
		public void OnClick() {
			TickSpeedAPI.SetTickPaused(false);
			TickSpeedAPI.SetTickSpeed(3f);
		}
	}
}