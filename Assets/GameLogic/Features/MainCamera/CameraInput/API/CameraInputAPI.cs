using System.Collections;
using GameLogic.World;
using NSFrame;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	public static class CameraInputAPI {
		private static CameraInputResource _inputRes;
		private static CameraInputResource InputRes => _inputRes ??= GameWorldMono.MainWorld.GetResource<CameraInputResource>();

		public static bool GetIsCameraInputEnabled() => InputRes.EnableCameraInput;

		private static int _lockCount = 0;


		public static void LockCameraInput() {
			++_lockCount;
			if (_lockCount == 1) {
				InputRes.EnableCameraInput = false;
			}
		}
		public static void UnlockCameraInput() {
			if (_lockCount > 0) {
				--_lockCount;
				if (_lockCount == 0) {
					InputRes.EnableCameraInput = true;
				}
			}
		}

		public static void TempLockInput(float time) {
			MonoService.Inst.StartCoroutine(LockInputCoro(time));
		}
		private static IEnumerator LockInputCoro(float time) {
			LockCameraInput();
			yield return new WaitForSecondsRealtime(time);
			UnlockCameraInput();
		}
	}
}