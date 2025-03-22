using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace NSFrame
{
	public class NSGraphEditor : EditorWindow {

		private NSGraphView _graphView;
		const string DEFAULT_FILE_NAME = "DefaultGraphName";
		private TextField _fileNameTF;
		private Button _saveButton, _openButton, _exportButton, _clearButton, _miniMapButton;

		[MenuItem("NSFrame/Tools/NS_GraphEditor")]
		public static void ShowEditorWindow() {
			GetWindow<NSGraphEditor>().titleContent = new GUIContent("NS Graph Editor");
		}

		public void CreateGUI() {
			AddGraphView();
			AddToolBar();
			AddStyles();
		}

	  #region Elements Addition
		private void AddGraphView() {
			_graphView = new(this);
			_graphView.StretchToParentSize();
			rootVisualElement.Add(_graphView);
		}
		private void AddToolBar() {
			Toolbar toolbar = new();

			_fileNameTF = GraphViewUtility.CreateTextField(DEFAULT_FILE_NAME, "File Name:");
			_saveButton = GraphViewUtility.CreateButton("Save", () => Save() );
			_openButton = GraphViewUtility.CreateButton("Import", () => Import() );
			_exportButton = GraphViewUtility.CreateButton("Export", () => Export() );
			_clearButton = GraphViewUtility.CreateButton("Clear", () => Clear() );
			_miniMapButton = GraphViewUtility.CreateButton("MiniMap", () => ToggleMiniMap() );

			toolbar.Add(_fileNameTF);
			toolbar.Add(_saveButton);
			toolbar.Add(_openButton);
			toolbar.Add(_exportButton);
			toolbar.Add(_clearButton);
			toolbar.Add(_miniMapButton);
			toolbar.AddStyleSheets("NS Graph Editor/ToolBar.uss");

			rootVisualElement.Add(toolbar);
		}
		private void AddStyles() {
			rootVisualElement.AddStyleSheets("NS Graph Editor/Variables.uss");
		}		
	  #endregion

	  #region Toolbar Actions
		private void Save() {
			if (string.IsNullOrEmpty(_fileNameTF.value)) {
				EditorUtility.DisplayDialog("Graph Save Error", "File name is invalid.", "OK");
				return;
			}
			IOUtility.SetTargetGraphView(_graphView);
			string folderPath = EditorUtility.SaveFolderPanel("选择保存的文件夹", Application.dataPath, string.Empty).GetRelativePath();
			AssetDatabase.Refresh();
			if (string.IsNullOrEmpty(folderPath)) {
				return;
			}
			IOUtility.Save(folderPath, _fileNameTF.value);
		}
		private void Import() {
			string fullPath = EditorUtility.OpenFilePanel("打开 Graph View", Application.dataPath, "asset").GetRelativePath();
			if (string.IsNullOrEmpty(fullPath)) {
				return;
			}
			var graphViewDataSO = AssetDatabase.LoadAssetAtPath<NSGraphViewDataSO>(fullPath);
			if (graphViewDataSO == null) {
				Debug.LogError($"Can't open the file at \"{fullPath}\".");
				return;
			}
			_graphView.Import(graphViewDataSO);
		}
		private void Export() {
			if (string.IsNullOrEmpty(_fileNameTF.value)) {
				EditorUtility.DisplayDialog("Graph Save Error", "File name is invalid.", "OK");
				return;
			}
			IOUtility.SetTargetGraphView(_graphView);
			string folderPath = EditorUtility.SaveFolderPanel("选择保存的文件夹", Application.dataPath, string.Empty).GetRelativePath();
			AssetDatabase.Refresh();
			if (string.IsNullOrEmpty(folderPath)) {
				return;
			}
			IOUtility.Export(folderPath, _fileNameTF.value);
		}
		private void Clear() {
			_fileNameTF.value = string.Empty;
			_graphView.ClearGraphView();
		}
		private void ToggleMiniMap() {
			_graphView.ToggleMiniMap();
		}
	  #endregion

	  #region Utility Methods
		public void EnableSaving() {
			_saveButton.SetEnabled(true);
			_exportButton.SetEnabled(true);
		}
		public void DisableSaving() {
			_saveButton.SetEnabled(false);
			_exportButton.SetEnabled(false);
		}
	  #endregion
	}
}
