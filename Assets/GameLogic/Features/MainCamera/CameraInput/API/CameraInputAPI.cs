using System.Collections;
using GameLogic.World;
using NSFrame;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	public static class CameraInputAPI {
		public static void SetCameraInputEnabled(bool enabled) {
			var res = GameWorldMono.MainWorld.GetResource<CameraInputResource>();
			res.EnableCameraInput = enabled;
		}
		public static void TempLockInput(float time) {
			MonoService.Inst.StartCoroutine(LockInputCoro(time));
		}
		private static IEnumerator LockInputCoro(float time) {
			CameraInputAPI.SetCameraInputEnabled(false);
			yield return new WaitForSecondsRealtime(time);
			CameraInputAPI.SetCameraInputEnabled(true);
		}
	}
}