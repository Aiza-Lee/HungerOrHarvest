using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.View {
	public class SpriteRendererComponent : IComponent {
		public float Alpha = 1f;
		public bool IsDirty = true;
		public Color Color = Color.white;
		public int SortingLayerID = SortingLayer.NameToID("Default");
		public int SortingOrder = 0;

		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;

		public void ApplyToSpriteRenderer(SpriteRenderer sr) {
			if (sr == null) return;
			sr.color = new Color(Color.r, Color.g, Color.b, Alpha);
			sr.sortingLayerID = SortingLayerID;
			sr.sortingOrder = SortingOrder;
		}
	}
}