using GameLogic.Resources.MainCamera;
using GameLogic.World;
using UnityEngine;

namespace GameLogic.Views.MainCamera {
	public class MainCameraMono : MonoBehaviour {
		[SerializeField] private MainCameraResource _mainCameraComp;

		void OnValidate() {
			var mainCameraRes = GameWorldMono.MainWorld.GetResource<MainCameraResource>();
			mainCameraRes.CopyFrom(_mainCameraComp);
		}
	}
}