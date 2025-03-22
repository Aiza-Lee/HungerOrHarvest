using UnityEngine;

namespace NSFrame {
	public abstract class MonoSingleton<T> : MonoBehaviour 
	where T : MonoSingleton<T> {
		public static T Inst { get; private set; }

		protected virtual void Awake() {
			if (Inst != null && Inst != this) {
				Destroy(gameObject);
				return ;
			}
			if (Inst == null) 
				Inst = this as T;
		}
	}
}