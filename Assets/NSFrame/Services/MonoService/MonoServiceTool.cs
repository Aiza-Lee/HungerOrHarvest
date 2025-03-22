using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace NSFrame {
	public static class MonoServiceTool {

		private class MonoService : MonoSingleton<MonoService> {
			private readonly UnityEvent Updating = new();
			private readonly UnityEvent LateUPdating = new();
			private readonly UnityEvent FiexdUpdating = new();

			public void AddUpdateListener(UnityAction action) {
				Updating.AddListener(action);
			}
			public void RemoveUpdateListener(UnityAction action) {
				Updating.RemoveListener(action);
			}
			public void AddLateUpdateListener(UnityAction action) {
				LateUPdating.AddListener(action);
			}
			public void RemoveLateUpdateListener(UnityAction action) {
				LateUPdating.RemoveListener(action);
			}
			public void AddFixedUpdateListener(UnityAction action) {
				FiexdUpdating.AddListener(action);
			}
			public void RemoveFixedUpdateListener(UnityAction action) {
				FiexdUpdating.RemoveListener(action);
			}

			private void Update() {
				Updating?.Invoke();
			}
			private void LateUpdate() {
				LateUPdating?.Invoke();
			}
			private void FixedUpdate() {
				FiexdUpdating?.Invoke();
			}
		}

		public static void NS_AddUpdListener(UnityAction action) {
			MonoService.Inst.AddUpdateListener(action);
		}
		public static void NS_RemoveUpdListener(UnityAction action) {
			MonoService.Inst.RemoveUpdateListener(action);
		}
		//LateUpdate
		public static void NS_AddLateUpdListener(UnityAction action) {
			MonoService.Inst.AddLateUpdateListener(action);
		}
		public static void NS_RemoveLateUpdListener(UnityAction action) {
			MonoService.Inst.RemoveLateUpdateListener(action);
		}
		//FixedUpdate
		public static void NS_AddFixedUpdListener(UnityAction action) {
			MonoService.Inst.AddFixedUpdateListener(action);
		}
		public static void NS_RemoveFixedUpdListener(UnityAction action) {
			MonoService.Inst.RemoveFixedUpdateListener(action);
		}
		//Coroutine
		public static Coroutine NS_StartCoroutine(IEnumerator routine) {
			return MonoService.Inst.StartCoroutine(routine);
		}
		public static void NS_StopCoroutine(Coroutine routine) {
			MonoService.Inst.StopCoroutine(routine);
		}
		public static void NS_StopCoroutine(IEnumerator routine) {
			MonoService.Inst.StopCoroutine(routine);
		}
		public static void NS_StopAllCoroutines() {
			MonoService.Inst.StopAllCoroutines();
		}
	}
}