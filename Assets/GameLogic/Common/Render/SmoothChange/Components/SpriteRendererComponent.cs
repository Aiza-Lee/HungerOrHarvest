using System;
using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	public class SpriteRendererComponent : IComponent {
		public float Alpha = 1f;
		public bool IsDirty = true; // 脏标记
		public void CopyFrom(IComponent other) {
			if (other is SpriteRendererComponent otherComp) {
				Alpha = otherComp.Alpha;
			} else {
				throw new ArgumentException("Cannot copy from non-SpriteRendererComponent component");
			}
		}
		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;
	}
}