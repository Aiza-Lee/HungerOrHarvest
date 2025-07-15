using UnityEngine;

namespace OldGameLogic.View
{
	public class AlphaController : MonoBehaviour {
		[Tooltip("0.x for Vill. 1f for Arch and Layer.")]public  float RATE = 0.1f;
		private SmoothFade[] _smoothFades;
		private Transform _cameraTrans;
		private bool _updating = true;

		private void Awake() {
			_smoothFades = GetComponentsInChildren<SmoothFade>();
			// _cameraTrans = WorldCameraMgr.Inst.Camera.transform;
		}

		private void Update() {
			var cmrZ = _cameraTrans.position.z;
			if (transform.position.z > cmrZ && (transform.position.z - cmrZ) <= ViewConstMgr.LayerGap) {
				_updating = true;
				var gap = ViewConstMgr.LayerGap;
				SetAlpha(Mathf.Clamp01(1f + ((transform.position.z - cmrZ - gap) / (gap * RATE))));
			} else if (_updating) {
				_updating = false;
				SetAlpha(1f);
			}
		}
		private void SetAlpha(float alpha) {
			foreach (var sf in _smoothFades) {
				sf.SetCurVal(alpha);
			}
		}
	}
}