using UnityEngine;
using NsEcsFrame.Core;
using NsEcsFrame.Components;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// Unity MonoBehaviour桥接器，用于在Unity游戏物体上关联ECS实体
	/// </summary>
	public class EntityBehaviour : MonoBehaviour {
		/// <summary>
		/// 所属世界的引用
		/// </summary>
		[SerializeField] private WorldBehaviour _worldBehaviour;

		/// <summary>
		/// 关联的ECS实体ID
		/// </summary>
		public EntityId EntityId;

		/// <summary>
		/// 是否自动同步Transform
		/// </summary>
		public bool SyncTransform = true;

		/// <summary>
		/// 在Unity中是否可见
		/// </summary>
		public bool IsVisible = true;

		/// <summary>
		/// 实体标签，用于标识实体类型
		/// </summary>
		public string EntityTag;

		/// <summary>
		/// 缓存Entity引用
		/// </summary>
		private Entity _entity;

		private void Start() {
			// 如果没有指定世界，尝试查找默认世界
			if (_worldBehaviour == null) {
				_worldBehaviour = FindFirstObjectByType<WorldBehaviour>();
			}

			if (_worldBehaviour != null && _worldBehaviour.World != null) {
				// 如果没有关联实体或实体无效，创建新实体
				if (EntityId.IsNull() || !_worldBehaviour.World.IsEntityAlive(EntityId)) {
					CreateEntity();
				} else {
					// 获取已存在的实体
					_entity = _worldBehaviour.World.GetEntity(EntityId);

					// 确保实体有基本组件
					EnsureBaseComponents();
				}
			}
		}

		private void Update() {
			if (_worldBehaviour == null || _worldBehaviour.World == null || _entity == null || !_entity.IsValid())
				return;

			// 同步Transform组件
			if (SyncTransform) {
				SyncTransformToEntity();
			}
		}

		private void OnDestroy() {
			// 当Unity对象销毁时，销毁对应的ECS实体
			DestroyEntity();
		}

		/// <summary>
		/// 创建新的ECS实体
		/// </summary>
		public void CreateEntity() {
			if (_worldBehaviour == null || _worldBehaviour.World == null)
				return;

			// 创建实体
			_entity = _worldBehaviour.World.CreateEntity();
			EntityId = _entity.EntityId;

			// 添加基本组件
			EnsureBaseComponents();

			// 添加标签组件
			if (!string.IsNullOrEmpty(EntityTag)) {
				var tagComponent = new TagComponent { Tag = EntityTag };
				_entity.AddComponent(tagComponent);
			}
		}

		/// <summary>
		/// 确保实体有基本的组件
		/// </summary>
		private void EnsureBaseComponents() {
			if (_entity == null)
				return;

			// 添加或更新Transform组件
			var transform = _entity.GetComponent<TransformComponent>();
			if (transform == null) {
				transform = new TransformComponent();
				_entity.AddComponent(transform);
			}

			// 初次同步Transform
			SyncTransformToEntity();
		}

		/// <summary>
		/// 将Unity Transform同步到ECS实体
		/// </summary>
		public void SyncTransformToEntity() {
			if (_entity == null)
				return;

			var transComp = _entity.GetComponent<TransformComponent>();
			if (transComp != null) {
				transComp.position = new Vector3d(transform.position.x, transform.position.y, transform.position.z);
				transComp.rotation = new Quaterniond(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
				transComp.scale = new Vector3d(transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}
		}

		/// <summary>
		/// 将ECS实体同步到Unity Transform
		/// </summary>
		public void SyncEntityToTransform() {
			if (_entity == null)
				return;

			var transComp = _entity.GetComponent<TransformComponent>();
			if (transComp != null) {
				transform.position = new Vector3((float) transComp.position.x, (float) transComp.position.y, (float) transComp.position.z);
				transform.rotation = new Quaternion((float) transComp.rotation.x, (float) transComp.rotation.y, (float) transComp.rotation.z, (float) transComp.rotation.w);
				transform.localScale = new Vector3((float) transComp.scale.x, (float) transComp.scale.y, (float) transComp.scale.z);
			}
		}

		/// <summary>
		/// 获取关联的实体
		/// </summary>
		public Entity GetEntity() {
			if (_entity == null && _worldBehaviour != null && _worldBehaviour.World != null && !EntityId.IsNull()) {
				_entity = _worldBehaviour.World.GetEntity(EntityId);
			}
			return _entity;
		}

		/// <summary>
		/// 销毁关联的实体
		/// </summary>
		public void DestroyEntity() {
			if (_entity != null && _entity.IsValid()) {
				_worldBehaviour.World.DestroyEntity(_entity.EntityId);
				_entity = null;
				EntityId = EntityId.NullEntityId;
			}
		}
	}
}
