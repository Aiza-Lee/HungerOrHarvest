using UnityEngine;
using NsEcsFrame.Core;

namespace NsEcsFrame.Components {
	/// <summary>
	/// 变换组件，仅存储本地位置、旋转和缩放数据
	/// </summary>
	public class TransformComponent : IComponent {
		/// <summary>本地位置</summary>
		public Vector3 LocalPosition;
		/// <summary>本地旋转</summary>
		public Quaternion LocalRotation;
		/// <summary>本地缩放</summary>
		public Vector3 LocalScale;

		public bool IsDirty = true; // 脏标记，初始为true，首次同步

		public TransformComponent() {
			LocalPosition = Vector3.zero;
			LocalRotation = Quaternion.identity;
			LocalScale = Vector3.one;
		}

		/// <summary>
		/// 创建默认变换组件
		/// </summary>
		public TransformComponent(
			Vector3? localPosition = null,
			Quaternion? localRotation = null,
			Vector3? localScale = null) {
			LocalPosition = localPosition ?? Vector3.zero;
			LocalRotation = localRotation ?? Quaternion.identity;
			LocalScale = localScale ?? Vector3.one;
		}

		/// <summary>
		/// 根据Unity Transform创建变换组件
		/// </summary>
		public TransformComponent(Transform transform) {
			LocalPosition = transform.localPosition;
			LocalRotation = transform.localRotation;
			LocalScale = transform.localScale;
		}

		/// <summary>
		/// 应用变换到Unity Transform
		/// </summary>
		public void ApplyToTransform(Transform transform) {
			if (transform == null) return;
			transform.localPosition = LocalPosition;
			transform.localRotation = LocalRotation;
			transform.localScale = LocalScale;
		}

		/// <summary>
		/// 从RectTransform读取本地属性（仅UI）
		/// </summary>
		public void ReadFromRectTransform(RectTransform rectTransform) {
			if (rectTransform == null) return;
			LocalPosition = rectTransform.localPosition;
			LocalRotation = rectTransform.localRotation;
			LocalScale = rectTransform.localScale;
		}

		/// <summary>
		/// 应用本地属性到RectTransform（仅UI）
		/// </summary>
		public void ApplyToRectTransform(RectTransform rectTransform) {
			if (rectTransform == null) return;
			rectTransform.localPosition = LocalPosition;
			rectTransform.localRotation = LocalRotation;
			rectTransform.localScale = LocalScale;
		}

		public void CopyFrom(IComponent other) {
			if (other is TransformComponent otherTransform) {
				LocalPosition = otherTransform.LocalPosition;
				LocalRotation = otherTransform.LocalRotation;
				LocalScale = otherTransform.LocalScale;
			} else {
				throw new System.InvalidCastException("Cannot copy from a component of different type.");
			}
		}

		public override string ToString() {
			return $"Local: Pos={LocalPosition}, Rot={LocalRotation}, Scale={LocalScale}";
		}

		public static TransformComponent FromTransform(Transform transform) => new TransformComponent(transform);

		public void MarkDirty() => IsDirty = true;
		public void ClearDirty() => IsDirty = false;
	}
}
