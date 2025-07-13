using NsEcsFrame.Core;
using UnityEngine;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// 在unity面板中直接调试Ecs中的Resource资源，会在初始化的时候覆盖world中注册的同类型资源。
	/// </summary>
	public abstract class ResourceMono<T> : MonoBehaviour where T : class, IResource {
		[SerializeField] protected T _resource;

		void Start() {
			if (WorldBehaviour.MainWorld == null) {
				Debug.LogError($"ResourceMono<{typeof(T).Name}>: MainWorld is not initialized.");
				return;
			}
			WorldBehaviour.MainWorld.InsertResource(_resource);
		}

		protected virtual void OnValidate() { }
	}
}