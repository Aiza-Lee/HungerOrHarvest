using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	public class NSFrameRoot : MonoSingleton<NSFrameRoot> {
		[SerializeField] private List<ConfigBase> _configs;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void InitializeOnLoad() {
			var rootGO = new GameObject("NSFrameRoot");
			var root = rootGO.AddComponent<NSFrameRoot>();
			rootGO.AddComponent<MonoService>();
			NSFrameRoot.Inst = root;
			DontDestroyOnLoad(root.gameObject);
		}

		protected override void Awake() {
			// note: 需要注意这样的话每个场景的配置都是不互通的，而是到那个场景就使用那个场景的配置
			// note: 虽然配置文件应该不会有太大改动，是在硬盘上的
			if (NSFrameRoot.Inst != null) {
				NSFrameRoot.Inst._configs = new(this._configs);
				Destroy(gameObject);
			}
		}

		public T GetConfig<T>() where T : ConfigBase {
			foreach (ConfigBase config in _configs)
				if (config is T t) return t;
			Debug.LogError($"NS: Can't find configuration named \"{typeof(T)}\".");
			return null;
		}
	}
}