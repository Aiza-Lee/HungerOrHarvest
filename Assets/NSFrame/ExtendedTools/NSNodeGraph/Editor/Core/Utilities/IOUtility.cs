using System.IO;
using UnityEditor;
using UnityEngine;

namespace NSFrame
{
	public static class IOUtility {
		private static NSGraphView _graphView;
		private static string _fileName;
		private static string _folderPath;

		public static void SetTargetGraphView(NSGraphView graphView) {
			_graphView = graphView;
		}

	  #region Save Methods
	  	/// <summary>
		/// 保存 Graph View 的方法
		/// </summary>
	  	public static void Save(string folderPath, string fileName) {
			_folderPath = folderPath;
			_fileName = fileName;
			_graphView.CreateViewDataSO(folderPath, fileName);
		}
	  #endregion

	  #region Export Methods
	  	/// <summary>
		/// 保存 Graph SO 的方法
		/// </summary>
		public static void Export(string folderPath, string fileName) {
			_folderPath = folderPath;
			_fileName = fileName;
			_graphView.CreateGraphSO(_folderPath, _fileName);
		}
	  #endregion

	  #region Assets Manage Methods
		public static T CreateSO<T>(string path, string soName) where T : ScriptableObject {
			string fullPath = $"{path}/{soName}.asset";
			T so = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(so, fullPath);
			return so;
		}
		public static T CreateSO<T>(Object assetObj) where T : ScriptableObject {
			T so = ScriptableObject.CreateInstance<T>();
			AssetDatabase.AddObjectToAsset(so, assetObj);
			return so;
		}
		public static T CreateSO<T>() where T : ScriptableObject {
			T so = ScriptableObject.CreateInstance<T>();
			return so;
		}
		public static void SaveAssets(Object obj) {
			EditorUtility.SetDirty(obj);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		private static string AddPath(this string ori, string addedString) {
			return $"{ori}/{addedString}";
		}
		public static string GetRelativePath(this string ori) {
			if (string.IsNullOrEmpty(ori)) {
				return null;
			}
			string rootPath = Application.dataPath;
			rootPath = rootPath[..rootPath.LastIndexOf('/')];
			return Path.GetRelativePath(rootPath, ori);
		}
	  #endregion
	}
}