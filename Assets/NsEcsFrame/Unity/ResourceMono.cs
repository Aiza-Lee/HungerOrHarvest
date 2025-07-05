using NsEcsFrame.Core;
using UnityEngine;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// 在unity面板中直接调试Ecs中的Resource资源。
	/// </summary>
	public abstract class ResourceMono<T> : MonoBehaviour where T : class, IResource {
		[SerializeField] private T _resource;

		void Start() {
			if (WorldBehaviour.MainWorld == null) {
				Debug.LogError($"ResourceMono<{typeof(T).Name}>: MainWorld is not initialized.");
				return;
			}
			if (WorldBehaviour.MainWorld.HasResource<T>()) {
				_resource = WorldBehaviour.MainWorld.GetResource<T>();
			} else {
				Debug.LogError($"ResourceMono<{typeof(T).Name}>: Resource not found in MainWorld.");
			}
		}
	}
}