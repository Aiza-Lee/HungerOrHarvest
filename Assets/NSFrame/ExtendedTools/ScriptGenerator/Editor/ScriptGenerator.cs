using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace NSFrame {
	public class ScriptGeneratorWindow : EditorWindow {

		private static List<string> _scriptsToGenerate = null;
		private static ScriptTemplate _template = null;

		[MenuItem("NSFrame/Tools/Generate Script")]
		private static void OpenWindow() {
			ScriptGeneratorWindow window = GetWindow<ScriptGeneratorWindow>("脚本生成");
			window.Show();

			_scriptsToGenerate = new(){ string.Empty, string.Empty };
		}
		
		private void OnGUI() {

			_template = (ScriptTemplate)EditorGUILayout.ObjectField("模板", _template, typeof(ScriptTemplate), false);

			EditorGUILayout.LabelField("生成列表", EditorStyles.boldLabel);
			for (int i = 0; i < _scriptsToGenerate.Count; ++i) {
				EditorGUILayout.BeginHorizontal();
				_scriptsToGenerate[i] = EditorGUILayout.TextField($"Item {i + 1}", _scriptsToGenerate[i]);
				if (GUILayout.Button("-", GUILayout.Width(20))) {
					_scriptsToGenerate.RemoveAt(i);
					EditorGUILayout.EndHorizontal();
					break;
				}
				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button("Add Item")){
				_scriptsToGenerate.Add("New Item");
			}

			EditorGUI.BeginDisabledGroup(_template == null);
			if (GUILayout.Button("生成")) 
				GenerateScript();
			EditorGUI.EndDisabledGroup();

			// if (_template == null)
			// 	EditorGUILayout.HelpBox("请选择一个模板", MessageType.Error);

		}
		private void GenerateScript() {

			string path = EditorUtility.SaveFolderPanel("选择保存的文件夹", Application.dataPath, string.Empty);
			if (string.IsNullOrEmpty(path)) 
				return;

			for (int i = 0; i < _scriptsToGenerate.Count; ++i) {
				string fileName = _scriptsToGenerate[i];
				string content = _template.Content.Replace("#SCRIPTNAME#", fileName);
				File.WriteAllText(Path.Combine(path, fileName + ".cs"), content);
				Debug.Log($"NS: 脚本\"{fileName}.cs\"生成成功");
			}
			AssetDatabase.Refresh();

			Close();
			Resources.UnloadUnusedAssets();
		}
	}
}