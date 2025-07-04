using GameLogic.Features.MainCamera;
using GameLogic.World;
using UnityEngine;

namespace GameLogic.Presentaion.MainCamera {
	public class MainCameraMono : MonoBehaviour {
		[SerializeField] private MainCameraResource _mainCameraComp;

		private void OnValidate() {
		}
	}
}