using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.UnityComponentsBridge {
	public class SpriteRendererComponent : IComponent, IDirtyMarker {
		public float Alpha = 1f;
		public Color Color = Color.white;
		public int SortingLayerID = SortingLayer.NameToID("Default");
		public int SortingOrder = 0;

		public bool Dirty = true;
		public void MarkDirty() => Dirty = true;
		public void ClearDirty() => Dirty = false;
		public bool IsDirty() => Dirty;

		public void ApplyToSpriteRenderer(SpriteRenderer sr) {
			if (sr == null) return;
			sr.color = new Color(Color.r, Color.g, Color.b, Alpha);
			sr.sortingLayerID = SortingLayerID;
			sr.sortingOrder = SortingOrder;
		}
	}
}