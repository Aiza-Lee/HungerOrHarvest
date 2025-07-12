using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;
using NsEcsFrame.Unity;

namespace GameLogic.Common.UnityComponentsBridge {
	public class SpriteRendererComponent : IComponent, IDirtyMarker {
		public SimpleColor Color = new(1f, 1f, 1f, 1f);
		public int SortingLayerID = SortingLayer.NameToID("Default");
		public int SortingOrder = 0;

		public bool Dirty = true;
		public void MarkDirty() => Dirty = true;
		public void ClearDirty() => Dirty = false;
		public bool IsDirty() => Dirty;

		public void ApplyToSpriteRenderer(SpriteRenderer sr) {
			sr.color = Color;
			sr.sortingLayerID = SortingLayerID;
			sr.sortingOrder = SortingOrder;
		}
	}
}