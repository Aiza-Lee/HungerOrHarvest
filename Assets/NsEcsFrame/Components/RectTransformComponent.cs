using UnityEngine;
using NsEcsFrame.Core;

namespace NsEcsFrame.Components {
	public class RectTransformComponent : IComponent {
		/// <summary>锚点最小值</summary>
		public Vector2 AnchorMin;
		/// <summary>锚点最大值</summary>
		public Vector2 AnchorMax;
		/// <summary>左下偏移</summary>
		public Vector2 OffsetMin;
		/// <summary>右上偏移</summary>
		public Vector2 OffsetMax;
		/// <summary>中心点</summary>
		public Vector2 Pivot;
		/// <summary>尺寸</summary>
		public Vector2 SizeDelta;
		/// <summary>锚点相对位置</summary>
		public Vector2 AnchoredPosition;

		public bool IsDirty = true; // 脏标记

		public RectTransformComponent() {
			AnchorMin = Vector2.zero;
			AnchorMax = Vector2.one;
			OffsetMin = Vector2.zero;
			OffsetMax = Vector2.zero;
			Pivot = new Vector2(0.5f, 0.5f);
			SizeDelta = Vector2.zero;
			AnchoredPosition = Vector2.zero;
		}

		public RectTransformComponent(RectTransform rect) {
			ReadFromRectTransform(rect);
		}

		public void ReadFromRectTransform(RectTransform rect) {
			if (rect == null) return;
			AnchorMin = rect.anchorMin;
			AnchorMax = rect.anchorMax;
			OffsetMin = rect.offsetMin;
			OffsetMax = rect.offsetMax;
			Pivot = rect.pivot;
			SizeDelta = rect.sizeDelta;
			AnchoredPosition = rect.anchoredPosition;
		}

		public void ApplyToRectTransform(RectTransform rect) {
			if (rect == null) return;
			rect.anchorMin = AnchorMin;
			rect.anchorMax = AnchorMax;
			rect.offsetMin = OffsetMin;
			rect.offsetMax = OffsetMax;
			rect.pivot = Pivot;
			rect.sizeDelta = SizeDelta;
			rect.anchoredPosition = AnchoredPosition;
		}

		public void CopyFrom(IComponent other) {
			if (other is RectTransformComponent otherRect) {
				AnchorMin = otherRect.AnchorMin;
				AnchorMax = otherRect.AnchorMax;
				OffsetMin = otherRect.OffsetMin;
				OffsetMax = otherRect.OffsetMax;
				Pivot = otherRect.Pivot;
				SizeDelta = otherRect.SizeDelta;
				AnchoredPosition = otherRect.AnchoredPosition;
			} else {
				throw new System.InvalidCastException("Cannot copy from a component of different type.");
			}
		}

		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;

		public override string ToString() {
			return $"AnchorMin={AnchorMin}, AnchorMax={AnchorMax}, OffsetMin={OffsetMin}, OffsetMax={OffsetMax}, Pivot={Pivot}, SizeDelta={SizeDelta}, AnchoredPosition={AnchoredPosition}";
		}
	}
}