using System.Collections.Generic;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Layer {
	public class LayerEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<LayerIdentityComponent>();
		}
	}
}