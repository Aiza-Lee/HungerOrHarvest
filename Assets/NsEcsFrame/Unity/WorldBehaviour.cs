using UnityEngine;
using NsEcsFrame.Core;
using System.Collections.Generic;
using NsEcsFrame.Systems;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// 在Unity中管理Ecs World的MonoBehaviour组件
	/// </summary>
	public class WorldBehaviour : MonoBehaviour {
		public static IWorld MainWorld { get; private set; }

		/// <summary>
		/// 是否为主World
		/// </summary>
		public bool isMainWorld = true;

		/// <summary>
		/// World名称
		/// </summary>
		public string worldName = "GameWorld";

		/// <summary>
		/// 是否启用调试日志
		/// </summary>
		public bool enableDebugLogs = false;

		/// <summary>
		/// Ecs World引用
		/// </summary>
		public IWorld World { get; private set; }

		/// <summary>
		/// World中的系统列表
		/// </summary>
		private readonly List<ISystem> _registeredSystems = new();

		private void Awake() {
			// 创建Ecs World
			World = new World(worldName) {
				EnableDebugLogs = enableDebugLogs
			};

			// 如果是主World，设为静态实例
			if (isMainWorld) {
				MainWorld = World;
			}

			RegisterCoreSystems();
			RegisterGameSystems();
		}

		private void Update() {
			// 更新所有系统
			World?.SystemManager.UpdateSystems(Time.deltaTime);
		}

		private void OnDestroy() {
			// 清理World
			if (World != null) {
				// 如果是主World，清除静态引用
				if (isMainWorld && MainWorld == World) {
					MainWorld = null;
				}

				// 销毁所有系统
				foreach (var system in _registeredSystems) {
					system?.OnDestroy();
				}

				_registeredSystems.Clear();

				// 销毁World中的所有实体
				World.Destroy();

				World = null;
			}
		}

		/// <summary>
		/// 注册核心系统
		/// </summary>
		private void RegisterCoreSystems() { }

		/// <summary>
		/// 注册游戏系统
		/// </summary>
		private void RegisterGameSystems() { }

		/// <summary>
		/// 注册系统
		/// </summary>
		public T RegisterSystem<T>() where T : class, ISystem, new() {
			var system = World.SystemManager.RegisterSystem<T>();

			// 如果是BaseSystem，需要初始化World引用
			if (system is BaseSystem baseSystem) {
				baseSystem.Initialize(World);
			}

			_registeredSystems.Add(system);
			return system;
		}

		/// <summary>
		/// 获取系统
		/// </summary>
		public T GetSystem<T>() where T : class, ISystem {
			return World.SystemManager.GetSystem<T>();
		}

		/// <summary>
		/// 静态主World引用
		/// </summary>
	}
}
