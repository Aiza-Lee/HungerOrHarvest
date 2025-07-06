using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.View {
	public class SpriteRendererComponent : IComponent {
		public float Alpha = 1f;
		public bool IsDirty = true;
		public Color Color = Color.white;

		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;

		public void ApplyToSpriteRenderer(SpriteRenderer spriteRenderer) {
			if (spriteRenderer == null) return;
			spriteRenderer.color = new Color(Color.r, Color.g, Color.b, Alpha);
		}
	}
}