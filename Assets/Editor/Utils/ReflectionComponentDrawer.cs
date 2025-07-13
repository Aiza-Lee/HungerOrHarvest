using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace Editor.Utils {
	/// <summary>
	/// 反射组件详情绘制器
	/// </summary>
	public static class ReflectionComponentDrawer {
		
		/// <summary>
		/// 使用反射绘制组件详情
		/// </summary>
		public static void DrawComponentDetailsWithReflection(object component) {
			var componentType = component.GetType();
			
			// 显示公共字段和属性
			var fields = componentType.GetFields(BindingFlags.Public | BindingFlags.Instance);
			var properties = componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

			if (fields.Length > 0) {
				EditorGUILayout.LabelField("字段:", EditorStyles.boldLabel);
				foreach (var field in fields) {
					try {
						var value = field.GetValue(component);
						EditorFieldDrawer.DrawFieldValue(field.Name, field.FieldType, value);
					} catch (Exception e) {
						EditorGUILayout.LabelField($"  {field.Name}: <获取失败: {e.Message}>");
					}
				}
			}

			if (properties.Any()) {
				EditorGUILayout.LabelField("属性:", EditorStyles.boldLabel);
				foreach (var property in properties) {
					try {
						var value = property.GetValue(component);
						EditorFieldDrawer.DrawFieldValue(property.Name, property.PropertyType, value);
					} catch (Exception e) {
						EditorGUILayout.LabelField($"  {property.Name}: <获取失败: {e.Message}>");
					}
				}
			}
		}
		
		/// <summary>
		/// 绘制组件详情（使用Unity序列化或反射）
		/// </summary>
		public static void DrawComponentDetails(object component) {
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);

			var componentType = component.GetType();
			EditorGUILayout.LabelField($"类型: {componentType.FullName}", EditorStyles.miniLabel);

			// 尝试使用Unity的序列化系统显示组件
			if (component is UnityEngine.Object unityObject) {
				// 如果是UnityEngine.Object，直接使用SerializedObject
				try {
					var serializedObject = new SerializedObject(unityObject);
					serializedObject.Update();
					
					var property = serializedObject.GetIterator();
					bool enterChildren = true;
					while (property.NextVisible(enterChildren)) {
						enterChildren = false;
						
						// 跳过script字段
						if (property.propertyPath == "m_Script") continue;
						
						EditorGUI.BeginDisabledGroup(true); // 设为只读
						EditorGUILayout.PropertyField(property, true);
						EditorGUI.EndDisabledGroup();
					}
					
					serializedObject.ApplyModifiedProperties();
				} catch (Exception e) {
					Debug.LogWarning($"使用Unity序列化显示组件失败: {e.Message}");
					DrawComponentDetailsWithReflection(component);
				}
			} else {
				// 对于ECS组件等非UnityEngine.Object，使用改进的反射方式
				DrawComponentDetailsWithReflection(component);
			}

			EditorGUILayout.EndVertical();
		}
	}
}
