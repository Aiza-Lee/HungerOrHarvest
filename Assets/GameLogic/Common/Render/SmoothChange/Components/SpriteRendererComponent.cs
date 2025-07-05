using System;
using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	public class SpriteRendererComponent : IComponent {
		public float Alpha = 1f;
		public bool IsDirty = true; // 脏标记
		
		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;
	}
}