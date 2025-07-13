using UnityEngine;
using System.Linq;
using NsEcsFrame.Unity;
using GameLogic.Common.DataTypes;

namespace Editor.Utils {
	/// <summary>
	/// 类型格式化工具类，用于将各种类型的值格式化为可读的字符串
	/// </summary>
	public static class TypeFormatter {

		private const int MaxCollectionItems = 10; // 最大显示集合项数

		/// <summary>
		/// 将值格式化为字符串
		/// </summary>
		public static string FormatValue(object value) {
			if (value == null) return "null";
			if (value is string str) return $"\"{str}\"";
			if (value is bool || value.GetType().IsPrimitive) return value.ToString();

			// 自定义类型
			if (value is OL ol) return $"OL:{ol}";
			if (value is Coord coord) return $"Coord:{coord}";

			// EtPair 类型处理 - 混合策略
			if (IsEtPairType(value.GetType())) {
				return FormatEtPair(value);
			}

			// Unity 和 Simple 类型的格式化
			if (value is Vector3 v3) return $"({v3.x:F2}, {v3.y:F2}, {v3.z:F2})";
			if (value is SimpleVector3 sv3) return $"({sv3.x:F2}, {sv3.y:F2}, {sv3.z:F2})";
			if (value is Vector2 v2) return $"({v2.x:F2}, {v2.y:F2})";
			if (value is SimpleVector2 sv2) return $"({sv2.x:F2}, {sv2.y:F2})";
			if (value is Vector4 v4) return $"({v4.x:F2}, {v4.y:F2}, {v4.z:F2}, {v4.w:F2})";
			if (value is Color color) return $"RGBA({color.r:F2}, {color.g:F2}, {color.b:F2}, {color.a:F2})";
			if (value is SimpleColor scolor) return $"RGBA({scolor.r:F2}, {scolor.g:F2}, {scolor.b:F2}, {scolor.a:F2})";

			// 集合类型的格式化
			if (CollectionTypeHelper.IsCollectionType(value.GetType())) {
				return FormatCollectionValue(value);
			}

			// 对于复杂对象，显示类型名
			return $"<{value.GetType().Name}>";
		}

		/// <summary>
		/// 格式化集合类型的值
		/// </summary>
		public static string FormatCollectionValue(object value) {
			if (value == null) return "null";

			if (value is not System.Collections.IEnumerable enumerable) return $"<{value.GetType().Name}>";

			try {
				var items = enumerable.Cast<object>().Take(MaxCollectionItems).ToArray();
				var totalCount = enumerable.Cast<object>().Count();

				if (totalCount == 0) {
					return $"{value.GetType().Name}[0]";
				} else if (totalCount <= MaxCollectionItems) {
					var itemsStr = string.Join(", ", items.Select(FormatValue));
					return $"{value.GetType().Name}[{totalCount}]: [{itemsStr}]";
				} else {
					var itemsStr = string.Join(", ", items.Select(FormatValue));
					return $"{value.GetType().Name}[{totalCount}]: [{itemsStr}, ...]";
				}
			} catch {
				return $"<{value.GetType().Name}[?]>";
			}
		}

		/// <summary>
		/// 检查是否为 EtPair 类型
		/// </summary>
		private static bool IsEtPairType(System.Type type) {
			return type.IsGenericType &&
				   type.GetGenericTypeDefinition().Name.StartsWith("EtPair");
		}

		/// <summary>
		/// 格式化 EtPair 类型的值
		/// </summary>
		private static string FormatEtPair(object value) {
			try {
				// 使用 switch 表达式改写类型匹配
				return value switch {
					EtPair<RepoType, bool> RbPair 	=> $"Pair:({RbPair.EnumType}, {RbPair.Value})",
					EtPair<RepoType, float> RfPair 	=> $"Pair:({RfPair.EnumType}, {RfPair.Value})",
					EtPair<RepoType, int> RiPair 	=> $"Pair:({RiPair.EnumType}, {RiPair.Value})",
					EtPair<JobType, int> JiPair 	=> $"Pair:({JiPair.EnumType}, {JiPair.Value})",
					EtPair<JobType, float> JfPair 	=> $"Pair:({JfPair.EnumType}, {JfPair.Value})",
					EtPair<JobType, bool> JbPair 	=> $"Pair:({JbPair.EnumType}, {JbPair.Value})",
					EtPair<ArchType, int> AiPair 	=> $"Pair:({AiPair.EnumType}, {AiPair.Value})",
					EtPair<ArchType, float> AfPair 	=> $"Pair:({AfPair.EnumType}, {AfPair.Value})",
					EtPair<ArchType, bool> AbPair 	=> $"Pair:({AbPair.EnumType}, {AbPair.Value})",
					_ => null // 继续后续反射处理
				} ?? FormatEtPairFallback(value);
			} catch (System.Exception ex) {
				return $"<EtPair:Error - {ex.Message}>";
			}
		}

		private static string FormatEtPairFallback(object value) {
			try {
				var type = value.GetType();
				var enumTypeProp = type.GetProperty("EnumType");
				var valueProp = type.GetProperty("Value");

				if (enumTypeProp != null && valueProp != null) {
					var entityId = enumTypeProp.GetValue(value, null);
					var componentType = valueProp.GetValue(value, null);
					return $"Pair:({FormatValue(entityId)}, {FormatValue(componentType)})";
				}

				return $"<EtPair:{type.Name}>";
			} catch (System.Exception ex) {
				return $"<EtPair:Error - {ex.Message}>";
			}
		}
	}
}
