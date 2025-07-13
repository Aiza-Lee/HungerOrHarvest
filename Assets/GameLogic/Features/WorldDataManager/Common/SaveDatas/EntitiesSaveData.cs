using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.WorldDataManager {
	public class EntitiesSaveData {
		public List<EntitySaveData> Entities = new();

		public EntitiesSaveData() { }

		public EntitiesSaveData(IWorld world) {
			var entities = world.GetAllEntities();
			foreach (var entity in entities) {
				if (entity.HasComponent<SavedEntityComponent>()) {
					Entities.Add(new(entity));
				}
			}
		}
	}

	public class EntitySaveData {
		public List<IComponent> Components = new();
		public EntitySaveData() { }
		public EntitySaveData(Entity entity) {
			var comps = entity.GetAllComponents();
			foreach (var comp in comps) {
				if (comp is ISaveIgnoreComponent) continue; // 跳过不需要保存的组件
				Components.Add(comp);
			}
		}
	}
}