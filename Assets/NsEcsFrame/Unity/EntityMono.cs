using NsEcsFrame.Core;
using UnityEngine;
using System.Collections.Generic;

namespace NsEcsFrame.Unity {
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