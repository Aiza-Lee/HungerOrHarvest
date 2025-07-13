using System.Collections.Generic;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Arch {
	public class ArchEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetSomeComponents(Entity entity) {
			yield return entity.GetComponent<VillContainerComponent>();
			yield return entity.GetComponent<BondToVillComponent>();
		}
	}
}