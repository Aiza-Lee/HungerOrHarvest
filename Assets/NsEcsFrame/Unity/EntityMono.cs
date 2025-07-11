using NsEcsFrame.Core;
using UnityEngine;
using System.Collections.Generic;

namespace NsEcsFrame.Unity {
	/// <summary>
	/// EntityMono 是一个抽象类，用于在Unity中表示一个实体（Entity）的MonoBehaviour。
	/// <para> 核心功能是: </para>
	/// <para> <list type="bullet">
	/// <item> 在运行时暴露entity的component于inspector </item>
	/// <item> 通过EntityId来获取EntityMono的实例,然后获取对应的unity组件并实现在场景中做出玩家可视的变化 </item>
	/// <item> 在Gameobject被Destroy的时候自动移除管理的映射 </item>
	/// </list> </para>
	/// </summary>
	public abstract class EntityMono : MonoBehaviour {
		private static readonly Dictionary<EntityId, EntityMono> _entityMap = new();
		public static EntityMono GetByEntityId(EntityId id) {
			_entityMap.TryGetValue(id, out var mono);
			return mono;
		}

		private EntityId _entityId;
		public EntityId EntityId => _entityId;

		[SerializeField][SerializeReference] private List<IComponent> _components = new();

		public void SetEntity(Entity entity) {
			_entityId = entity.ID;
			_entityMap[_entityId] = this;
			_components.Clear();
			foreach (var component in GetAllComponents(entity)) {
				_components.Add(component);
			}
		}

		void OnDestroy() {
			_components.Clear();
			_entityMap.Remove(_entityId);
		}

		/// <summary>
		/// 返回所有需要在unity Inspector中调试的Entity的组件的引用的集合。
		/// </summary>
		protected abstract IEnumerable<IComponent> GetAllComponents(Entity entity);
	}
}