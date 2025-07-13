using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using NsEcsFrame.Unity;

namespace Editor.Utils {
	/// <summary>
	/// Editor字段绘制工具类
	/// </summary>
	public static class EditorFieldDrawer {
		
		/// <summary>
		/// 根据类型绘制字段值
		/// </summary>
		public static void DrawFieldValue(string name, Type type, object value) {
			EditorGUI.BeginDisabledGroup(true);
			
			// 根据类型使用合适的Editor控件
			if (type == typeof(int)) {
				EditorGUILayout.IntField($"  {name}", value != null ? (int)value : 0);
			} else if (type == typeof(float)) {
				EditorGUILayout.FloatField($"  {name}", value != null ? (float)value : 0f);
			} else if (type == typeof(bool)) {
				EditorGUILayout.Toggle($"  {name}", value != null && (bool)value);
			} else if (type == typeof(string)) {
				EditorGUILayout.TextField($"  {name}", value?.ToString() ?? "");
			} else if (type == typeof(Vector2)) {
				EditorGUILayout.Vector2Field($"  {name}", value != null ? (Vector2)value : Vector2.zero);
			} else if (type == typeof(SimpleVector2)) {
				EditorGUILayout.Vector2Field($"  {name}", value != null ? (SimpleVector2)value : Vector2.zero);
			} else if (type == typeof(Vector3)) {
				EditorGUILayout.Vector3Field($"  {name}", value != null ? (Vector3)value : Vector3.zero);
			} else if (type == typeof(SimpleVector3)) {
				EditorGUILayout.Vector3Field($"  {name}", value != null ? (SimpleVector3)value : Vector3.zero);
			} else if (type == typeof(Vector4)) {
				EditorGUILayout.Vector4Field($"  {name}", value != null ? (Vector4)value : Vector4.zero);
			} else if (type == typeof(SimpleQuaternion)) {
				EditorGUILayout.Vector4Field($"  {name}", value != null ? (SimpleQuaternion)value : Vector4.zero);
			} else if (type == typeof(Color)) {
				EditorGUILayout.ColorField($"  {name}", value != null ? (Color)value : Color.white);
			} else if (type == typeof(SimpleColor)) {
				EditorGUILayout.ColorField($"  {name}", value != null ? (SimpleColor)value : Color.white);
			} else if (type.IsEnum) {
				EditorGUILayout.EnumPopup($"  {name}", value != null ? (Enum)value : (Enum)Enum.GetValues(type).GetValue(0));
			} else if (typeof(UnityEngine.Object).IsAssignableFrom(type)) {
				EditorGUILayout.ObjectField($"  {name}", value as UnityEngine.Object, type, false);
			} else if (CollectionTypeHelper.IsCollectionType(type)) {
				DrawCollectionValue(name, value);
			} else {
				// 对于其他类型，显示字符串表示
				EditorGUILayout.LabelField($"  {name}: {TypeFormatter.FormatValue(value)}");
			}
			EditorGUI.EndDisabledGroup();
		}
		
		/// <summary>
		/// 绘制集合值
		/// </summary>
		public static void DrawCollectionValue(string name, object value) {
			if (value == null) {
				EditorGUILayout.LabelField($"  {name}: null");
				return;
			}

			if (value is not System.Collections.IEnumerable enumerable) {
				EditorGUILayout.LabelField($"  {name}: <无法枚举>");
				return;
			}

			var items = enumerable.Cast<object>().ToArray();
			var count = items.Length;
			
			// 显示集合基本信息
			EditorGUILayout.LabelField($"  {name} ({value.GetType().Name}): {count} 项");
			
			// 如果项目较少，显示前几项的详情
			if (count > 0 && count <= 5) {
				EditorGUI.indentLevel++;
				for (int i = 0; i < count; i++) {
					var item = items[i];
					EditorGUILayout.LabelField($"    [{i}]: {TypeFormatter.FormatValue(item)}");
				}
				EditorGUI.indentLevel--;
			} else if (count > 5) {
				EditorGUI.indentLevel++;
				// 显示前3项
				for (int i = 0; i < 3; i++) {
					var item = items[i];
					EditorGUILayout.LabelField($"    [{i}]: {TypeFormatter.FormatValue(item)}");
				}
				EditorGUILayout.LabelField($"    ... 还有 {count - 3} 项");
				EditorGUI.indentLevel--;
			}
		}
	}
}
