using System.Collections.Generic;
using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[RequireComponent(typeof(SpriteRenderer))]
	public class VillEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<VillVitalityComponent>();
			yield return entity.GetComponent<VillIdentityComponent>();
			yield return entity.GetComponent<CoordComponent>();
			yield return entity.GetComponent<JobExpComponent>();
		}
	}
}