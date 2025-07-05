using System;
using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	public class SpriteRendererComponent : IComponent {
		public float Alpha = 1f;
		public void CopyFrom(IComponent other) {
			if (other is SpriteRendererComponent otherComp) {
				Alpha = otherComp.Alpha;
			} else {
				throw new ArgumentException("Cannot copy from non-SpriteRendererComponent component");
			}
		}
	}
}