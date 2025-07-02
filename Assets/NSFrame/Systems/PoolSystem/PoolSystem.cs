using System;
using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	public static class PoolSystem {
		private static readonly Dictionary<string, PrefabPool> _prefabPoolDic;
		private static readonly Dictionary<Type, IObjectPool> _objectPoolDic;
		private static readonly Transform _poolRootTransform;

		static PoolSystem() {
			_prefabPoolDic = new();
			_objectPoolDic = new();
			_poolRootTransform = new GameObject("Pool Root").transform;
			_poolRootTransform.position = Vector3.zero;
			_poolRootTransform.SetParent(NSFrameRoot.Inst.transform);
		}

	  #region Prefab Pool
		private static bool CheckPrefabContained(string name) {
			if (_prefabPoolDic.ContainsKey(name)) return true;
			Debug.LogError($"NS: Have not initialize the \"{name}\" Prefab Pool");
			return false;
		}

		public static void InitPrefabPool(GameObject go, int maxCapacity = -1) {
			string name = go.name;
			if (_prefabPoolDic.ContainsKey(name)) {
				// Debug.LogError($"NS: Have areadily initialized the \"{name}\" Prefab Pool");
				return;
			}
			_prefabPoolDic.Add(name, new(go, _poolRootTransform, maxCapacity));
		}

		public static GameObject PopGO(GameObject go, Transform father = null) {
			return PopGO(go.name, father);
		}
		public static GameObject PopGO(string name, Transform father = null) {
			if (CheckPrefabContained(name)) 
				return _prefabPoolDic[name].Pop(father);
			return null;
		}
		public static T PopGO<T>(GameObject go, Transform father = null) where T : Component {
			return PopGO<T>(go.name, father);
		}
		public static T PopGO<T>(string name, Transform father = null) where T : Component {
			if (CheckPrefabContained(name)) 
				return _prefabPoolDic[name].Pop(father).GetComponent<T>();
			return null;
		}

		public static void PushGO(GameObject go) {
			string name = go.name;
			if (CheckPrefabContained(name)) 
				_prefabPoolDic[name].Push(go);
		}

		public static void SetMaxCapacity(string name, int maxCapacity) {
			if (CheckPrefabContained(name)) 
				_prefabPoolDic[name].SetMaxCapacity(maxCapacity);
		}
	  #endregion

	  #region Object Pool
		private static bool CheckObjectContained(Type type) {
			if (_objectPoolDic.ContainsKey(type)) return true;
			Debug.LogError($"NS: Have not initialize the \"{type}\" Object Pool");
			return false;
		}

		public static void InitObjectPool<T>(int maxCapacity = -1) where T : class, IPooledObject, new() {
			Type type = typeof(T);
			if (_objectPoolDic.ContainsKey(type)) {
				// Debug.LogError($"NS: Have areadily initialized the \"{type}\" Object Pool");
				return;
			}
			_objectPoolDic.Add(type, new ObjectPool<T>(maxCapacity));
		}

		public static T PopObj<T>() where T : class, new() {
			return PopObj(typeof(T)) as T;
		}
		public static object PopObj(Type type) {
			if (CheckObjectContained(type)) return _objectPoolDic[type].Pop();
			return null;
		}

		public static void PushObj<T>(T obj) where T : class, new() {
			PushObj(typeof(T), obj);
		}
		public static void PushObj(Type type, object obj) {
			if (CheckObjectContained(type)) _objectPoolDic[type].Push(obj);
		}

		public static void SetMaxCapacity(Type type, int maxCapacity) {
			if (CheckObjectContained(type)) _objectPoolDic[type].SetMaxCapacity(maxCapacity);
		}
	  #endregion
	}
}


