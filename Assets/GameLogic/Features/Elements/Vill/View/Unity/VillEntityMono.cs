using System.Collections.Generic;
using GameLogic.Common.View;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[RequireComponent(typeof(SpriteRenderer))]
	public class VillEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<VillStatComponent>();
			yield return entity.GetComponent<VillIdentityComponent>();
			yield return entity.GetComponent<SmoothedCoordComponent>();
			yield return entity.GetComponent<JobExpComponent>();
		}
	}
}