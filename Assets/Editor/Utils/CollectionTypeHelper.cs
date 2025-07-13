using System;
using System.Collections.Generic;

namespace Editor.Utils {
	/// <summary>
	/// 集合类型辅助工具类
	/// </summary>
	public static class CollectionTypeHelper {
		
		/// <summary>
		/// 检测是否为集合类型
		/// </summary>
		public static bool IsCollectionType(Type type) {
			if (type.IsArray) return true;
			if (type.IsGenericType) {
				var genericTypeDef = type.GetGenericTypeDefinition();
				return genericTypeDef == typeof(List<>) ||
				       genericTypeDef == typeof(HashSet<>) ||
				       genericTypeDef == typeof(Dictionary<,>) ||
				       typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
			}
			return typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string);
		}
	}
}
