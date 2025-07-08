using System;
using System.Collections.Generic;
using System.Linq;
using NsEcsFrame.Utils;
using UnityEngine;

namespace NsEcsFrame.Core {
	public class World : IWorld {
		private readonly IComponentManager _componentManager;
		private readonly ISystemManager _systemManager;
		private readonly IEventBus _eventBus;
		private readonly SparseSet<Entity> _aliveEntities = new();
		private readonly Stack<uint> _recycledIds = new();
		private uint _nextEntityId = 1; // 0 保留为无效ID
		private readonly Dictionary<EntityId, uint> _entityVersions = new();
		private readonly Dictionary<Type, IResource> _resources = new();

		public string Name { get; }
		public bool EnableDebugLogs { get; set; } = false;

		public IComponentManager ComponentManager => _componentManager;
		public ISystemManager SystemManager => _systemManager;
		public IEventBus EventBus => _eventBus;

		/// <summary>
		/// 创建新的ECS世界
		/// </summary>
		/// <param name="name">世界名称</param>
		public World(string name = "DefaultWorld") {
			Name = name;
			_componentManager = new ComponentManager();
			_systemManager = new SystemManager(this);
			_eventBus = new EventBus();
		}

		public Entity CreateEntity() {
			uint id;

			// 使用回收的ID或分配新ID
			if (_recycledIds.Count > 0) {
				id = _recycledIds.Pop();
			} else {
				id = _nextEntityId++;
			}

			// 版本号默认为1，如果是复用ID则递增
			uint version = 1;
			EntityId entityId = new(id, version);

			// 如果是复用的ID，版本号递增
			if (_entityVersions.ContainsKey(entityId)) {
				version = _entityVersions[entityId] + 1;
				entityId = new EntityId(id, version);
			}

			_entityVersions[entityId] = version;

			Entity entity = new(this, entityId, _componentManager);
			_aliveEntities.Add(entityId.ID, entity);

			if (EnableDebugLogs) {
				Debug.Log($"[{Name}] Created entity: {entityId}");
			}

			return entity;
		}
		public void DestroyEntity(EntityId entityId) {
			if (!_aliveEntities.Contains(entityId.ID)) {
				if (EnableDebugLogs) {
					Debug.LogWarning($"[{Name}] Trying to destroy non-existent entity: {entityId}");
				}
				return;
			}
			// 清除所有组件
			_componentManager.RemoveAllComponents(entityId);
			// 从活跃实体字典中移除
			_aliveEntities.Remove(entityId.ID);
			// 回收ID
			_recycledIds.Push(entityId.ID);

			if (EnableDebugLogs) {
				Debug.Log($"[{Name}] Destroyed entity: {entityId}");
			}
		}

		public bool IsEntityAlive(EntityId entityId) => _aliveEntities.Contains(entityId.ID);
		public Entity GetEntity(EntityId entityId) => _aliveEntities.Get(entityId.ID);
		public IReadOnlyCollection<Entity> GetAllEntities() => _aliveEntities.ToList();
		public int EntityCount => _aliveEntities.Count;

		public void RenderUpdate(float deltaTime) =>_systemManager.RenderUpdate(deltaTime);
		public void LogicUpdate(float deltaTime) => _systemManager.LogicUpdate(deltaTime);

		public void Destroy() {
			// 销毁所有系统
			_systemManager.DestroyAllSystems();
			// 销毁所有实体
			foreach (var entity in _aliveEntities.ToList()) {
				DestroyEntity(entity.ID);
			}
			// 清理事件总线
			_eventBus.Clear();
			
			if (EnableDebugLogs) {
				Debug.Log($"[{Name}] World destroyed");
			}
		}

		public IWorld InsertResource<T>(T resource) where T : class, IResource {
			var type = typeof(T);
			_resources[type] = resource;
			return this;
		}
		public T GetResource<T>() where T : class, IResource {
			var type = typeof(T);
			if (_resources.TryGetValue(type, out var res) && res is T typedRes) {
				return typedRes;
			}
			throw new KeyNotFoundException($"Resource of type {type.Name} not found in world {Name}");
		}
		public bool TryGetResource<T>(out T resource) where T : class, IResource {
			var type = typeof(T);
			if (_resources.TryGetValue(type, out var res) && res is T typedRes) {
				resource = typedRes;
				return true;
			}
			resource = default;
			return false;
		}
		public bool HasResource<T>() where T : class, IResource {
			var type = typeof(T);
			return _resources.ContainsKey(type);
		}
		public bool RemoveResource<T>() where T : class, IResource {
			var type = typeof(T);
			return _resources.Remove(type);
		}

		public EntityQueryBuilder CreateQueryBuilder() => new(this);

		/// <summary>
		/// 执行实体查询
		/// </summary>
		/// <param name="withAllTypes">必须包含的组件类型</param>
		/// <param name="withAnyTypes">至少包含一种的组件类型</param>
		/// <param name="withoutTypes">不能包含的组件类型</param>
		/// <returns>查询结果</returns>
		internal EntityQuery Query(List<Type> withAllTypes, List<Type> withAnyTypes, List<Type> withoutTypes) {
			var result = new List<Entity>();

			// 如果没有任何筛选条件，返回所有实体
			if (withAllTypes.Count == 0 && withAnyTypes.Count == 0 && withoutTypes.Count == 0) {
				result.AddRange(_aliveEntities);
				return new EntityQuery(this, result);
			}

			// 如果有必须包含的组件类型，先获取包含第一个类型的实体
			if (withAllTypes.Count > 0) {
				var firstType = withAllTypes[0];
				var entityIds = _componentManager.GetEntitiesWith(firstType);

				foreach (var eId in entityIds) {
					if (_aliveEntities.TryGetValue(eId.ID, out var entity)) {
						bool hasAllComp = true;
						// 检查其他必须包含的组件类型
						for (int i = 1; i < withAllTypes.Count; i++) {
							if (!_componentManager.HasComponent(eId, withAllTypes[i])) {
								hasAllComp = false;
								break;
							}
						}
						if (hasAllComp) {
							result.Add(entity);
						}
					}
				}
			} else if (withAnyTypes.Count > 0) {
				HashSet<EntityId> legalEIds = new();

				foreach (var type in withAnyTypes) {
					var entities = _componentManager.GetEntitiesWith(type);
					legalEIds.UnionWith(entities);
				}

				foreach (var eId in legalEIds) {
					if (_aliveEntities.TryGetValue(eId.ID, out var entity)) {
						result.Add(entity);
					}
				}
			} else {
				// 如果只有不能包含的组件类型，从所有实体开始筛选
				result.AddRange(_aliveEntities);
			}

			// 过滤不能包含的组件类型
			if (withoutTypes.Count > 0) {
				result.RemoveAll(entity => {
					foreach (var type in withoutTypes) {
						if (_componentManager.HasComponent(entity.ID, type)) {
							return true;
						}
					}
					return false;
				});
			}

			return new EntityQuery(this, result);
		}
	}
}
