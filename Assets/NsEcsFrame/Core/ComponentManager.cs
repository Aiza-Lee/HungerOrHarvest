using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	public class ComponentManager : IComponentManager {
		private readonly Dictionary<Type, IComponentStorage> _componentStorages = new();
		private readonly Dictionary<EntityId, HashSet<Type>> _entityComponentTypes = new();

		public void RegisterComponentType<T>() where T : class, IComponent, new() {
			Type type = typeof(T);
			if (!_componentStorages.ContainsKey(type)) {
				_componentStorages[type] = new ComponentStorage<T>();
			}
		}

		public T AddComponent<T>(EntityId entityID) where T : class, IComponent, new() {
			return AddComponent(entityID, new T());
		}
		public T AddComponent<T>(EntityId entityID, T component) where T : class, IComponent {
			Type type = typeof(T);

			// 确保已注册Component类型
			if (!_componentStorages.ContainsKey(type)) {
				_componentStorages[type] = new ComponentStorage<T>();
			}

			// 获取Component存储
			var storage = (ComponentStorage<T>) _componentStorages[type];

			// 添加Component
			storage.AddComponent(entityID, component);

			// 记录Entity拥有的Component类型
			if (!_entityComponentTypes.TryGetValue(entityID, out var componentTypes)) {
				componentTypes = new HashSet<Type>();
				_entityComponentTypes[entityID] = componentTypes;
			}
			componentTypes.Add(type);

			return component;
		}

		public bool RemoveComponent<T>(EntityId entityID) where T : class, IComponent {
			Type type = typeof(T);

			if (!_componentStorages.TryGetValue(type, out var storage)) {
				return false;
			}

			bool result = storage.RemoveComponent(entityID);

			if (result && _entityComponentTypes.TryGetValue(entityID, out var componentTypes)) {
				componentTypes.Remove(type);
				if (componentTypes.Count == 0) {
					_entityComponentTypes.Remove(entityID);
				}
			}

			return result;
		}

		public T GetComponent<T>(EntityId entityId) where T : class, IComponent {
			Type type = typeof(T);

			if (!_componentStorages.TryGetValue(type, out var storage)) {
				return null;
			}

			return (T) storage.GetComponent(entityId);
		}

		public bool HasComponent<T>(EntityId entityId) where T : class, IComponent {
			return HasComponent(entityId, typeof(T));
		}
		public bool HasComponent(EntityId entityId, Type componentType) {
			if (!_componentStorages.TryGetValue(componentType, out var storage)) {
				return false;
			}

			return storage.HasComponent(entityId);
		}

		public IReadOnlyCollection<Type> GetComponentTypes(EntityId entityId) {
			if (_entityComponentTypes.TryGetValue(entityId, out var componentTypes)) {
				return componentTypes;
			}

			return Array.Empty<Type>();
		}

		public void RemoveAllComponents(EntityId entityId) {
			if (_entityComponentTypes.TryGetValue(entityId, out var componentTypes)) {
				foreach (var type in componentTypes) {
					_componentStorages[type].RemoveComponent(entityId);
				}

				_entityComponentTypes.Remove(entityId);
			}
		}

		public IReadOnlyCollection<EntityId> GetEntitiesWith<T>() where T : class, IComponent {
			Type type = typeof(T);

			if (!_componentStorages.TryGetValue(type, out var storage)) {
				return Array.Empty<EntityId>();
			}

			return storage.GetEntities();
		}

		public IReadOnlyCollection<EntityId> GetEntitiesWith(Type componentType) {
			if (!_componentStorages.TryGetValue(componentType, out var storage)) {
				return Array.Empty<EntityId>();
			}

			return storage.GetEntities();
		}

		public IReadOnlyCollection<T> GetAllComponents<T>() where T : class, IComponent {
			Type type = typeof(T);
			if (!_componentStorages.TryGetValue(type, out var storage)) {
				return Array.Empty<T>();
			}
			if (storage is ComponentStorage<T> typedStorage) {
				return typedStorage.GetAllComponents();
			}
			return Array.Empty<T>();
		}

		public IReadOnlyCollection<IComponent> GetAllComponents(Type componentType) {
			if (!_componentStorages.TryGetValue(componentType, out var storage)) {
				return Array.Empty<IComponent>();
			}
			if (storage is IComponentStorageWithAll withAll) {
				return withAll.GetAllComponents();
			}
			return Array.Empty<IComponent>();
		}

		/// <summary>
		/// Component存储接口
		/// </summary>
		private interface IComponentStorage {
			/// <summary>
			/// 是否包含特定Entity的Component
			/// </summary>
			bool HasComponent(EntityId entityId);
			/// <summary>
			/// 获取特定Entity的Component
			/// </summary>
			IComponent GetComponent(EntityId entityId);
			/// <summary>
			/// 添加Component到特定Entity
			/// </summary>
			bool RemoveComponent(EntityId entityId);
			/// <summary>
			/// 获取所有包含该Component类型的EntityID
			/// </summary>
			IReadOnlyCollection<EntityId> GetEntities();
		}

		private interface IComponentStorageWithAll {
			IReadOnlyCollection<IComponent> GetAllComponents();
		}

		/// <summary>
		/// 泛型Component存储实现
		/// </summary>
		/// <typeparam name="T">存储的Component的类型</typeparam>
		private class ComponentStorage<T> : IComponentStorage, IComponentStorageWithAll where T : class, IComponent {
			private readonly Dictionary<EntityId, T> _components = new();

			public void AddComponent(EntityId entityId, T component) {
				_components[entityId] = component;
			}

			public bool HasComponent(EntityId entityId) {
				return _components.ContainsKey(entityId);
			}

			public IComponent GetComponent(EntityId entityId) {
				_components.TryGetValue(entityId, out T component);
				return component;
			}

			public bool RemoveComponent(EntityId entityId) {
				return _components.Remove(entityId);
			}

			public IReadOnlyCollection<EntityId> GetEntities() {
				return _components.Keys;
			}

			public IReadOnlyCollection<T> GetAllComponents() {
				return _components.Values;
			}
			IReadOnlyCollection<IComponent> IComponentStorageWithAll.GetAllComponents() {
				return new List<IComponent>(_components.Values);
			}
		}
	}
}
