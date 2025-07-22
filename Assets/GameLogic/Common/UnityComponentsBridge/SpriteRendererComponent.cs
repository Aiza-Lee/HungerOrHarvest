using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;
using NsEcsFrame.Unity;

namespace GameLogic.Common.UnityComponentsBridge {
	[System.Serializable]
	public class SpriteRendererComponent : IComponent, IDirtyMarker {
		public SimpleColor Color;
		public int SortingLayerID;
		public int SortingOrder;

		public SimpleVector2 Size;
		public SpriteDrawMode DrawMode;
		public SpriteTileMode TileMode;
		public bool FlipX;
		public bool FlipY;

		public bool Dirty = true;
		public void MarkDirty() => Dirty = true;
		public void ClearDirty() => Dirty = false;
		public bool IsDirty() => Dirty;

		public SpriteRendererComponent() {
			Color = new SimpleColor(1f, 1f, 1f, 1f);
			SortingLayerID = SortingLayer.NameToID("Default");
			SortingOrder = 0;
			DrawMode = SpriteDrawMode.Simple;
			TileMode = SpriteTileMode.Continuous;
			FlipX = false;
			FlipY = false;
		}

		public SpriteRendererComponent(SpriteRenderer sr) {
			Color = sr.color;
			SortingLayerID = sr.sortingLayerID;
			SortingOrder = sr.sortingOrder;
			Size = sr.size;
			DrawMode = sr.drawMode;
			TileMode = sr.tileMode;
			FlipX = sr.flipX;
			FlipY = sr.flipY;
		}

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