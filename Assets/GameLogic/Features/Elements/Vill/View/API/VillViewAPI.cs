using GameLogic.Common.DataTypes;
using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	public static class VillViewAPI {
		public static void SetDirection(Entity vill, Coord direction) {
			var sr = vill.GetComponent<SpriteRendererComponent>();
			if (direction.X >= 0) {
				if (sr.FlipX != false) {
					sr.FlipX = false;
					sr.Dirty = true;
				}
			} else if (direction.X < 0) {
				if (sr.FlipX != true) {
					sr.FlipX = true;
					sr.Dirty = true;
				}
			}
		}

		public static void PlayAnimation(Entity vill, string animationName) {
			var go = EntityMono.GetByEntityId(vill.ID);
			go.GetComponent<Animator>().Play(animationName);
		}

		public static void StopAnimation(Entity vill) {
			var go = EntityMono.GetByEntityId(vill.ID);
			go.GetComponent<Animator>().StopPlayback();
		}
	}
}