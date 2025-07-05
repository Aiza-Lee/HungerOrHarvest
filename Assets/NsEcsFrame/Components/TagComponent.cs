using System.Collections.Generic;
using NsEcsFrame.Core;

namespace NsEcsFrame.Components {
	/// <summary>
	/// 标签组件，用于存储实体的标识信息
	/// </summary>
	public class TagComponent : IComponent  {
		/// <summary>
		/// 实体标签
		/// </summary>
		public string Tag;

		/// <summary>
		/// 实体名称
		/// </summary>
		public string Name;

		/// <summary>
		/// 创建默认标签组件
		/// </summary>
		public TagComponent() {
			Tag = "Default";
			Name = "Entity";
		}

		/// <summary>
		/// 根据标签和名称创建标签组件
		/// </summary>
		public TagComponent(string tag, string name) {
			Tag = tag;
			Name = name;
		}

		public void CopyFrom(IComponent other) {
			if (other is TagComponent otherTag) {
				Tag = otherTag.Tag;
				Name = otherTag.Name;
			} else {
				throw new System.InvalidCastException("Cannot copy from a component of different type.");
			}
		}
	}
}
