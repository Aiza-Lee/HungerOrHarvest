using System.Collections.Generic;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Elements.Decorations {
	public class DecorationEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetSomeComponents(Entity entity) {
			yield break;
		}
	}
}