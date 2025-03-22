using UnityEditor;
using UnityEngine.UIElements;

namespace NSFrame
{
	public static class StyleUtility {
		public static VisualElement AddClasses(this VisualElement element, params string[] classNames) {
			foreach (string className in classNames) {
				element.AddToClassList(className);
			}
			return element;
		}
		public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames) {
			foreach (string styleSheetName in styleSheetNames) {
				StyleSheet nodeViewStyleSheet = EditorGUIUtility.Load(styleSheetName) as StyleSheet;
				element.styleSheets.Add(nodeViewStyleSheet);
			}
			return element;
		}
    }
}
