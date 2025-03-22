using System.Collections;
using UnityEngine;

namespace GameLogic
{
	[RequireComponent(typeof(SmoothMove), typeof(SmoothCameraSize))]
	public class CameraController : MonoBehaviour {
		private bool _moveable = true;
		private ViewConstConfig ViewConfig => ViewConstMgr.Inst.Config;
		
		[SerializeField] private CameraSize _cameraSize;
		public CameraSize CameraSize { 
			get => _cameraSize; 
			set {
				_cameraSize = value;
				SmoothCameraSize.SetTarget(ViewConstMgr.Inst.Config.CameraSizes[(int)_cameraSize]); 
			}
		}

		public SmoothMove SmoothMove { get; private set; }
		public SmoothCameraSize SmoothCameraSize { get; private set; }

		private void Awake() {
			SmoothMove = GetComponent<SmoothMove>();
			SmoothCameraSize = GetComponent<SmoothCameraSize>();
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.F1)) {
				CameraSize = CameraSize.Focus;
			} else if (Input.GetKeyDown(KeyCode.F2)) {
				CameraSize = CameraSize.Normal;
			} else if (Input.GetKeyDown(KeyCode.F3)) {
				CameraSize = CameraSize.Wide;
			} else if (Input.GetKeyDown(KeyCode.F4)) {
				CameraSize = CameraSize.WideWide;
			}

			if (_moveable) {
				if (Input.GetKey(KeyCode.A)) {
					SmoothMove.TranslateCurVal(- ViewConfig.CAMERA_MOVE_SPEED * Time.deltaTime * Vector3.right);
				}
				if (Input.GetKeyUp(KeyCode.A)) {
					SmoothMove.Translate(- ViewConfig.CAMERA_STOP_LENGTH * Vector3.right);
				}
				if (Input.GetKey(KeyCode.D)) {
					SmoothMove.TranslateCurVal(ViewConfig.CAMERA_MOVE_SPEED * Time.deltaTime * Vector3.right);
				}
				if (Input.GetKeyUp(KeyCode.D)) {
					SmoothMove.Translate(ViewConfig.CAMERA_STOP_LENGTH * Vector3.right);
				}

				if (Input.GetKeyDown(KeyCode.W)) {
					SmoothMove.StopCur();
					SmoothMove.Translate(ConstMgr.Y_PER_LYR * ViewConfig.VZ_LY_RATE * Vector3.forward);
					StartCoroutine(LockLeftRight(SmoothMove.Configs[0].Time));
				}
				if (Input.GetKeyUp(KeyCode.S)) {
					SmoothMove.StopCur();
					SmoothMove.Translate(ConstMgr.Y_PER_LYR * ViewConfig.VZ_LY_RATE * Vector3.back);
					StartCoroutine(LockLeftRight(SmoothMove.Configs[0].Time));
				}
			}

		}
		IEnumerator LockLeftRight(float time) {
			_moveable = false;
			yield return new WaitForSecondsRealtime(time);
			_moveable = true;
		}
	}
}