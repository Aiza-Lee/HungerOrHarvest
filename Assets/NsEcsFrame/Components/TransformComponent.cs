using UnityEngine;
using NsEcsFrame.Core;

namespace NsEcsFrame.Components {
	/// <summary>
	/// 变换组件，存储位置、旋转和缩放数据
	/// </summary>
	public class TransformComponent : IComponent {
		/// <summary>
		/// 位置
		/// </summary>
		public Vector3 Position;

		/// <summary>
		/// 旋转
		/// </summary>
		public Quaternion Rotation;

		/// <summary>
		/// 缩放
		/// </summary>
		public Vector3 Scale;

		/// <summary>
		/// 创建默认变换组件
		/// </summary>
		public TransformComponent() {
			Position = Vector3.zero;
			Rotation = Quaternion.identity;
			Scale = Vector3.one;
		}

		/// <summary>
		/// 根据位置、旋转和缩放创建变换组件
		/// </summary>
		public TransformComponent(Vector3 position, Quaternion rotation, Vector3 scale) {
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		/// <summary>
		/// 根据Unity Transform创建变换组件
		/// </summary>
		public TransformComponent(Transform transform) {
			Position = transform.position;
			Rotation = transform.rotation;
			Scale = transform.localScale;
		}

		/// <summary>
		/// 应用变换到Unity Transform
		/// </summary>
		public void ApplyToTransform(Transform transform) {
			transform.position = Position;
			transform.rotation = Rotation;
			transform.localScale = Scale;
		}

		public void CopyFrom(IComponent other) {
			if (other is TransformComponent otherTransform) {
				Position = otherTransform.Position;
				Rotation = otherTransform.Rotation;
				Scale = otherTransform.Scale;
			} else {
				throw new System.InvalidCastException("Cannot copy from a component of different type.");
			}
		}
	}
}
