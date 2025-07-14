using UnityEngine;
using NsEcsFrame.Core;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// 在Unity中管理Ecs World的MonoBehaviour组件
	/// </summary>
	public abstract class WorldBehaviour : MonoBehaviour {
		public static IWorld MainWorld { get; private set; }

		/// <summary>
		/// 是否为主World
		/// </summary>
		[SerializeField] private bool _isMainWorld = true;

		/// <summary>
		/// World名称
		/// </summary>
		[SerializeField] private string _worldName = "GameWorld";

		/// <summary>
		/// 是否启用调试日志
		/// </summary>
		[SerializeField] private bool _enableDebugLogs = true;

		/// <summary>
		/// Ecs World引用
		/// </summary>
		public IWorld World { get; private set; }

		void Awake() {
			// 创建Ecs World
			World = new World(_worldName) {
				EnableDebugLogs = _enableDebugLogs
			};

			// 如果是主World，设为静态实例
			if (_isMainWorld) {
				MainWorld = World;
			}

			RegisterResources();
			RegisterSystems();
		}

		void Update() {
			World?.SystemManager.RenderUpdate(Time.unscaledDeltaTime);
		}
		void FixedUpdate() {
			World?.SystemManager.LogicUpdate(Time.fixedDeltaTime);
		}

		void OnDestroy() {
			if (World != null) {
				if (_isMainWorld && MainWorld == World) {
					MainWorld = null;
				}

				World.Destroy();
				World = null;
			}
		}

		/// <summary>
		/// 注册核心系统
		/// </summary>
		protected abstract void RegisterSystems();
		/// <summary>
		/// 注册资源
		/// </summary>
		protected abstract void RegisterResources();
	}
}
