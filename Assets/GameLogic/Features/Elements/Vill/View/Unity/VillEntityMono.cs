using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	[RequireComponent(typeof(SpriteRenderer))]
	public class VillEntityMono : EntityMono {
		protected override IEnumerable<IComponent> GetSomeComponents(Entity entity) {
			yield return entity.GetComponent<GidComponent>();
			yield return entity.GetComponent<SpriteRendererComponent>();
		}

		void Start() {
			var animator = GetComponent<Animator>();
			animator.speed = Random.Range(0.99f, 1.01f); // 随机化动画速度
			animator.Play("Walk", 0, Random.Range(0f, 1f)); // 随机播放Walk动画
		}
	}
}