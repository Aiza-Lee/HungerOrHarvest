using GameLogic.Common.DataTypes;
using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

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
	}
}