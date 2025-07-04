using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	public class SystemManager : ISystemManager {
		private readonly Dictionary<Type, ISystem> _systems = new();
		private readonly List<ISystem> _orderedSystems = new();
		private bool _needsOrdering = false;
		private readonly World _world;

		public SystemManager(World world) {
			_world = world;
		}

		public T RegisterSystem<T>() where T : class, ISystem, new() {
			Type type = typeof(T);

			if (_systems.TryGetValue(type, out var existingSystem)) {
				return (T) existingSystem;
			}

			var system = new T();
			_systems[type] = system;
			_orderedSystems.Add(system);
			_needsOrdering = true;

			system.OnCreate();

			return system;
		}

		public T RegisterSystem<T>(T system) where T : class, ISystem {
			Type type = typeof(T);

			if (_systems.TryGetValue(type, out var existingSystem)) {
				// 如果已存在，先销毁现有系统
				existingSystem.OnDestroy();
				_orderedSystems.Remove(existingSystem);
			}

			_systems[type] = system;
			_orderedSystems.Add(system);
			_needsOrdering = true;

			system.OnCreate();

			return system;
		}

		public T GetSystem<T>() where T : class, ISystem {
			Type type = typeof(T);

			if (_systems.TryGetValue(type, out var system)) {
				return (T) system;
			}

			return null;
		}

		public void SetSystemEnabled<T>(bool enabled) where T : class, ISystem {
			var system = GetSystem<T>();
			if (system != null) {
				system.Enabled = enabled;
			}
		}

		public void LogicUpdate(float deltaTime) {
			if (_needsOrdering) {
				// 根据优先级排序系统
				_orderedSystems.Sort((a, b) => a.Priority.CompareTo(b.Priority));
				_needsOrdering = false;
			}

			foreach (var system in _orderedSystems) {
				if (system.Enabled) {
					system.OnLogicUpdate(deltaTime);
				}
			}
		}

		public void RenderUpdate(float deltaTime) {
			if (_needsOrdering) {
				// 根据优先级排序系统
				_orderedSystems.Sort((a, b) => a.Priority.CompareTo(b.Priority));
				_needsOrdering = false;
			}

			foreach (var system in _orderedSystems) {
				if (system.Enabled) {
					system.OnRenderUpdate(deltaTime);
				}
			}
		}

		public void DestroyAllSystems() {
			foreach (var system in _systems.Values) {
				system.OnDestroy();
			}

			_systems.Clear();
			_orderedSystems.Clear();
		}
	}
}
