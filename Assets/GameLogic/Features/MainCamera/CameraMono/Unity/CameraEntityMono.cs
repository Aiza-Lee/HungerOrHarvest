using System.Collections.Generic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.MainCamera {
	public class CameraEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<MainCameraComponent>();
			yield return entity.GetComponent<TransformComponent>();
		}

		void Start() {
			var entity = GameWorldMono.MainWorld.CreateEntity();
			entity
				.AddComponent<TransformComponent>()
				.AddComponent<MainCameraComponent>()
				.AddComponent<CameraComponent>()
				.AddComponent<SmoothChangeStatComponent>()
			;
			SetEntity(entity);
		}
	}
}