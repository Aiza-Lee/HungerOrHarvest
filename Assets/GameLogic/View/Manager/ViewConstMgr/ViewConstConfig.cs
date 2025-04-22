using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.View
{
	[CreateAssetMenu(fileName = "ViewConstConfig", menuName = "HungerOrHarvest/Config/View/ViewConstConfig")]
	public class ViewConstConfig : ScriptableObject {

		public float CAMERA_MOVE_SPEED;
		public float CAMERA_STOP_LENGTH;
		public List<float> CameraSizes;

		public Gradient EnvironmentLightColor_Day;
		public Gradient EnvironmentLightColor_Night;

	}
}