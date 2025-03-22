using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "ViewConstConfig", menuName = "HungerOrHarvest/Config/View/ViewConstConfig")]
	public class ViewConstConfig : ScriptableObject {
		public float VX_LX_RATE;
		public float VZ_LY_RATE;
		public float DEFAULT_Y;

		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;

	}
}