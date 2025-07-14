using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;
using NsEcsFrame.Unity;

namespace GameLogic.Common.UnityComponentsBridge {
	public class SpriteRendererComponent : IComponent, IDirtyMarker {
		public SimpleColor Color = new(1f, 1f, 1f, 1f);
		public int SortingLayerID = SortingLayer.NameToID("Default");
		public int SortingOrder = 0;

		public SimpleVector2 Size;
		public SpriteDrawMode DrawMode = SpriteDrawMode.Simple;
		public SpriteTileMode TileMode = SpriteTileMode.Continuous;
		public bool FlipX = false;
		public bool FlipY = false;

		public bool Dirty = true;
		public void MarkDirty() => Dirty = true;
		public void ClearDirty() => Dirty = false;
		public bool IsDirty() => Dirty;

		public void ApplyToSpriteRenderer(SpriteRenderer sr) {
			sr.color = Color;
			sr.sortingLayerID = SortingLayerID;
			sr.sortingOrder = SortingOrder;
			sr.size = Size;
			sr.drawMode = DrawMode;
			sr.tileMode = TileMode;
			sr.flipX = FlipX;
			sr.flipY = FlipY;
		}
	}
}