using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.SaveLoadData {
	public class EntitiesSaveData {
		public List<EntitySaveData> Entities = new();

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
		public EntitySaveData(Entity entity) {
			var comps = entity.GetAllComponents();
			foreach (var comp in comps) {
				Components.Add(comp);
			}
		}
	}
}