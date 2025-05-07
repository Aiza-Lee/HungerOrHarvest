using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace NSFrame {
	public static class MonoServiceTool {

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