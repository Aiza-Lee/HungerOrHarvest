// using UnityEngine;
// using UnityEditor;

// namespace EditorUtility
// {
// 	public class ReadOnlyAttribute : PropertyAttribute { }

// 	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
// 	public class ReadOnlyDrawer : PropertyDrawer {
// 		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
// 			GUI.enabled = false;

// 			if (property.propertyType == SerializedPropertyType.String) {
// 				property.stringValue = EditorGUI.TextArea(position, property.stringValue);
// 			} else {
// 				EditorGUI.PropertyField(position, property, label, true);
// 			}

// 			GUI.enabled = true;
// 		}

// 		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
// 			if (property.propertyType == SerializedPropertyType.String) {
// 				return EditorGUIUtility.singleLineHeight * 3;
// 			}
// 			return EditorGUI.GetPropertyHeight(property, label, true);
// 		}
// 	}
// }

