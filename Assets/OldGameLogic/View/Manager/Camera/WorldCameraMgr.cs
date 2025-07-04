using System.Collections;
using OldGameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.View
{
	[RequireComponent(typeof(SmoothMove), typeof(SmoothCameraSize))]
	public class WorldCameraMgr : MonoSingleton<WorldCameraMgr>, IPlayerControll {
		private bool _moveable = true;
		private ViewConstConfig ViewConfig => ViewConstMgr.GetConfig;

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
		public Camera Camera { get; private set; }

		protected override void Awake() {
			base.Awake();
			Camera = GetComponent<Camera>();
			SmoothMove = GetComponent<SmoothMove>();
			SmoothCameraSize = GetComponent<SmoothCameraSize>();
			transform.position = new(0, transform.position.y, -ViewConstMgr.LayerGap);
		}

		private void Update() {
			if (Controllable) {
				UpdatePlayerControll();
			}
		}

		private void UpdatePlayerControll() {
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
					SmoothMove.TranslateCurVal(-ViewConfig.CAMERA_MOVE_SPEED * Time.unscaledDeltaTime * Vector3.right);
				}
				if (Input.GetKeyUp(KeyCode.A)) {
					SmoothMove.Translate(-ViewConfig.CAMERA_STOP_LENGTH * Vector3.right);
				}
				if (Input.GetKey(KeyCode.D)) {
					SmoothMove.TranslateCurVal(ViewConfig.CAMERA_MOVE_SPEED * Time.unscaledDeltaTime * Vector3.right);
				}
				if (Input.GetKeyUp(KeyCode.D)) {
					SmoothMove.Translate(ViewConfig.CAMERA_STOP_LENGTH * Vector3.right);
				}

				if (Input.GetKeyDown(KeyCode.W)) {
					SmoothMove.EndCurChange();
					SmoothMove.Translate(ConstMgr.Y_PER_LYR * ViewConstMgr.VZ_MY_RATE * Vector3.forward);
					StartCoroutine(LockMoveCoro(SmoothMove.Configs[0].Time));
				}
				if (Input.GetKeyUp(KeyCode.S)) {
					SmoothMove.EndCurChange();
					SmoothMove.Translate(ConstMgr.Y_PER_LYR * ViewConstMgr.VZ_MY_RATE * Vector3.back);
					StartCoroutine(LockMoveCoro(SmoothMove.Configs[0].Time));
				}
			}
		}

		IEnumerator LockMoveCoro(float time) {
			_moveable = false;
			yield return new WaitForSecondsRealtime(time);
			_moveable = true;
		}

		#region IPlayerControll
		public bool Controllable { get; set; } = false;
		#endregion
	}
}