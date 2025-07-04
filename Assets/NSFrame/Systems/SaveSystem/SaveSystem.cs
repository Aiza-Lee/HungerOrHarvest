using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.Plastic.Newtonsoft.Json;

namespace NSFrame {
	/// <summary>
	/// 存档管理器，外界通过需要持有一个SaveInfo对象来操作特定的存档
	/// </summary>
	public static class SaveSystem {

		private static readonly bool _prettyPrint = false;

		private const string SAVE_EXTENSION = ".json";
		private const string SAVE_DIR_NAME = "saveData";
		private const string SETTING_DIR_NAME = "setting";

		private static readonly string SAVE_DIR;
		private static readonly string SETTING_DIR;
		private static readonly List<SaveInfo> _saveInfoList = new();

		private static ISerializationStrategy _serializer;
		public static void SetSerializationStrategy(ISerializationStrategy strategy) {
			_serializer = strategy;
		}
		private static ISerializationStrategy Serializer {
			get {
				_serializer ??= new NewtonsoftSerializationStrategy();
				return _serializer;
			}
		}

		static SaveSystem() {
#if UNITY_EDITOR
			_prettyPrint = true;
#endif
			SAVE_DIR = Path.Combine(Application.persistentDataPath, SAVE_DIR_NAME);
			SETTING_DIR = Path.Combine(Application.persistentDataPath, SETTING_DIR_NAME);
			CheckDir(SAVE_DIR);
			CheckDir(SETTING_DIR);
			ReloadSaveInfoList();
		}

		/// <summary>
		/// 初始化saveinfos列表
		/// </summary>
		static void ReloadSaveInfoList() {
			DirectoryInfo dirInfo = new(SAVE_DIR);
			DirectoryInfo[] subDirs = dirInfo.GetDirectories();
			foreach (DirectoryInfo subDirInfo in subDirs) {
				FileInfo[] fileInfos = subDirInfo.GetFiles();
				for (int i = 0; i < fileInfos.Length; ++i)
					if (fileInfos[i].GetNameWithoutExtension() == typeof(SaveInfo).Name) {
						SaveInfo saveInfo = LoadFromFile<SaveInfo>(SAVE_DIR.AddPath(subDirInfo.Name).AddPath(typeof(SaveInfo).Name));
						_saveInfoList.Add(saveInfo);
						break;
					}
			}
		}

		#region 关于存档

		public static SaveInfo CreateSaveFile(string saveName = "DefaultSaveName") {
			string randomFile = RandomFile();
			SaveInfo saveInfo = new(randomFile, saveName, DateTime.Now.ToString());
			_saveInfoList.Add(saveInfo);
			SaveObject(saveInfo, saveInfo);
			return saveInfo;
		}
		public static void DeleteSaveFile(SaveInfo saveInfo) {
			for (int i = 0; i < _saveInfoList.Count; ++i) if (_saveInfoList[i] == saveInfo) {
					_saveInfoList.RemoveAt(i);
					break;
				}
			Directory.Delete(SAVE_DIR.AddPath(saveInfo.DirName), true);
		}
		private static string RandomFile() {
			return Path.GetRandomFileName()[..8] + Path.GetRandomFileName()[..8] + Path.GetRandomFileName()[..8];
		}

		public static List<SaveInfo> GetAllSaveInfos() {
			_saveInfoList.Sort(CmpByUpdateTime);
			return new(_saveInfoList);
		}
		private static int CmpByUpdateTime(SaveInfo saveInfo1, SaveInfo saveInfo2) {
			return (-1) * string.Compare(saveInfo1.LastUpdateTime, saveInfo2.LastUpdateTime);
		}

		#endregion

		#region 关于对象

		public static void SaveObject(SaveInfo saveInfo, string fileName, object obj) {
			string dir = SAVE_DIR.AddPath(saveInfo.DirName);
			CheckDir(dir);
			SaveToFile(obj, dir.AddPath(fileName));
			saveInfo.Update(DateTime.Now.ToString());
			SaveToFile(saveInfo, dir.AddPath(typeof(SaveInfo).Name));
		}
		public static void SaveObject(SaveInfo saveInfo, object obj) {
			SaveObject(saveInfo, obj.GetType().Name, obj);
		}
		public static void SaveObjects(SaveInfo saveInfo, params object[] objects) {
			foreach (object obj in objects)
				SaveObject(saveInfo, obj);
		}

		public static T LoadObject<T>(SaveInfo saveInfo, string fileName) {
			T obj = LoadFromFile<T>(SAVE_DIR.AddPath(saveInfo.DirName).AddPath(fileName));
			return obj;
		}
		public static T LoadObject<T>(SaveInfo saveInfo) {
			return LoadObject<T>(saveInfo, typeof(T).Name);
		}


		public static object LoadObject(SaveInfo saveInfo, string fileName, Type type) {
			object obj = LoadFromFile(SAVE_DIR.AddPath(saveInfo.DirName).AddPath(fileName), type);
			return obj;
		}
		public static object LoadObject(SaveInfo saveInfo, Type type) {
			return LoadObject(saveInfo, type.Name, type);
		}

		#endregion

		#region 全局数据

		public static T LoadSetting<T>(string fileName) {
			return LoadFromFile<T>(SETTING_DIR.AddPath(fileName));
		}
		public static T LoadSetting<T>() {
			return LoadSetting<T>(typeof(T).Name);
		}
		public static void SaveSetting(object obj, string fileName) {
			SaveToFile(obj, SETTING_DIR.AddPath(fileName));
		}
		public static void SaveSetting(object obj) {
			SaveSetting(obj, obj.GetType().Name);
		}

		#endregion

		#region 工具函数

		/// <summary>
		/// 确保目录存在，没有则创建
		/// </summary>
		private static void CheckDir(string path) {
			if (Directory.Exists(path)) return;
			Directory.CreateDirectory(path);
		}

		private static void SaveToFile(object obj, string path) {
			try {
				File.WriteAllText(path + SAVE_EXTENSION, Serializer.Serialize(obj, _prettyPrint));
			} catch (Exception ex) {
				Debug.LogError($"SaveSystem: SaveToFile failed: {ex.Message}\n{ex.StackTrace}");
			}
		}
		private static T LoadFromFile<T>(string path) {
			path += SAVE_EXTENSION;
			if (!File.Exists(path)) return default;
			try {
				return Serializer.Deserialize<T>(File.ReadAllText(path));
			} catch (Exception ex) {
				Debug.LogError($"SaveSystem: LoadFromFile<{typeof(T).Name}> failed: {ex.Message}\n{ex.StackTrace}");
				return default;
			}
		}
		private static object LoadFromFile(string path, Type type) {
			path += SAVE_EXTENSION;
			if (!File.Exists(path)) return null;
			try {
				return Serializer.Deserialize(File.ReadAllText(path), type);
			} catch (Exception ex) {
				Debug.LogError($"SaveSystem: LoadFromFile({type.Name}) failed: {ex.Message}\n{ex.StackTrace}");
				return null;
			}
		}

		#endregion

		#region Extension Functions

		private static string AddPath(this string ori, string path) {
			return Path.Combine(ori, path);
		}
		private static string GetNameWithoutExtension(this FileInfo fileInfo) {
			return Path.GetFileNameWithoutExtension(fileInfo.FullName);
		}

		#endregion

	}

	/// <summary>
	/// Newtonsoft.Json 序列化策略实现
	/// </summary>
	public class NewtonsoftSerializationStrategy : ISerializationStrategy {
		private static readonly JsonSerializerSettings Settings = new() {
			TypeNameHandling = TypeNameHandling.Auto
		};
		public string Serialize(object obj, bool prettyPrint = false) {
			var formatting = prettyPrint ? Formatting.Indented : Formatting.None;
			return JsonConvert.SerializeObject(obj, formatting, Settings);
		}
		public T Deserialize<T>(string json) {
			return JsonConvert.DeserializeObject<T>(json, Settings);
		}
		public object Deserialize(string json, Type type) {
			return JsonConvert.DeserializeObject(json, type, Settings);
		}
	}
}
