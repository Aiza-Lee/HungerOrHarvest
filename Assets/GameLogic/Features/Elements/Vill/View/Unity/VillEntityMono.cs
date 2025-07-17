using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Common.View;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[RequireComponent(typeof(SpriteRenderer))]
	public class VillEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetSomeComponents(Entity entity) {
			yield return entity.GetComponent<CoordComponent>();
			yield return entity.GetComponent<SmoothPositionStatComponent>();
			yield return entity.GetComponent<RoutePlanComponent>();
			yield return entity.GetComponent<VillVitalityComponent>();
			yield return entity.GetComponent<VillIdentityComponent>();
			yield return entity.GetComponent<JobExpComponent>();
		}

		void Start() {
			var animator = GetComponent<Animator>();
			animator.speed = Random.Range(0.99f, 1.01f); // 随机化动画速度
			animator.Play("Walk", 0, Random.Range(0f, 1f)); // 随机播放Walk动画
		}
	}
}