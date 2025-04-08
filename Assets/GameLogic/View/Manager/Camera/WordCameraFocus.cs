using GameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace GameLogic.View
{
	public class WorldCameraFocus : MonoSingleton<WorldCameraFocus> {
		private Transform _curFocus;
		private bool _IsFocusing = false;
		private float _lastSetTargetTime = 0;
		private const float SetTargetGapTime = .1f;

		private WorldCameraMgr MainMgr => WorldCameraMgr.Inst;

		private void Update() {
			if (_IsFocusing) {
				UpdateFocus();
			}
		}

		private void UpdateFocus() {
			if (_curFocus == null) {
				Debug.LogWarning("Camera focus target lost.");
				_IsFocusing = false;
				return;
			}
			if (Time.time - _lastSetTargetTime < SetTargetGapTime) return;
			_lastSetTargetTime = Time.time;
			var target = _curFocus.position - ViewConstMgr.LayerGap * Vector3.forward;
			MainMgr.SmoothMove.SetTarget(new(target.x, transform.position.y, target.z));
		}

		#region PublicMethods
		public void FocusOn(Transform target) {
			_IsFocusing = true;
			_curFocus = target;
			MainMgr.CameraSize = CameraSize.Focus;
		}
		public void FreeView() {
			if (!_IsFocusing) return;
			_IsFocusing = false;
			_curFocus = null;
			MainMgr.CameraSize = CameraSize.Normal;
			MainMgr.SmoothMove.SetCurVal(new(transform.position.x, transform.position.y, transform.position.GetBackLyrZ()));
		}
		#endregion
	}
}