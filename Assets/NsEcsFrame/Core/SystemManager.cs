using System;
using System.Collections.Generic;
using UnityEngine;

namespace NsEcsFrame.Core {
	public class SystemManager : ISystemManager {
		private readonly Dictionary<Type, ISystem> _systems = new();
		private readonly List<ISystem> _orderedSystems = new();
		private bool _needsOrdering = false;
		private readonly World _world;

		public SystemManager(World world) {
			_world = world;
		}

		public ISystemManager RegisterSystem<T>() where T : class, ISystem, new() {
			Type type = typeof(T);

			if (_systems.ContainsKey(type)) {
				if (_world.EnableDebugLogs) {
					Debug.Log($"System {type.Name} is already registered.");
				}
				return this;
			}

			var system = new T();
			system.Initialize(_world);
			_systems[type] = system;
			_orderedSystems.Add(system);
			_needsOrdering = true;

			system.OnCreate();
			if (_world.EnableDebugLogs) {
				Debug.Log($"System {type.Name} registered successfully.");
			}
			return this;
		}

		public ISystemManager RegisterSystem<T>(T system) where T : class, ISystem {
			Type type = typeof(T);

			if (_systems.TryGetValue(type, out var existingSystem)) {
				existingSystem.OnDestroy();
				_orderedSystems.Remove(existingSystem);
				if (_world.EnableDebugLogs) {
					Debug.Log($"System {type.Name} is being replaced.");
				}
			}

			_systems[type] = system;
			_orderedSystems.Add(system);
			_needsOrdering = true;

			system.OnCreate();
			if (_world.EnableDebugLogs) {
				Debug.Log($"System {type.Name} registered successfully.");
			}
			return this;
		}

		public T GetSystem<T>() where T : class, ISystem {
			Type type = typeof(T);

			if (_systems.TryGetValue(type, out var system)) {
				return (T) system;
			}
			if (_world.EnableDebugLogs) {
				Debug.LogError($"System {type.Name} is not registered.");
			}
			return null;
		}

		public void SetSystemEnabled<T>(bool enabled) where T : class, ISystem {
			var system = GetSystem<T>();
			if (system != null) {
				system.Enabled = enabled;
				if (_world.EnableDebugLogs) {
					Debug.Log($"System {typeof(T).Name} enabled state set to {enabled}.");
				}
			}
		}

		public void LogicUpdate(float deltaTime) {
			if (_needsOrdering) {
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
