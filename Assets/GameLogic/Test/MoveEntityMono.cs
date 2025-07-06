using System.Collections.Generic;
using GameLogic.Common.View;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Test {
	public class MoveEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<TransformComponent>();
			yield return entity.GetComponent<SmoothChangeStatComponent>();
		}
	}
}