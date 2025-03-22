using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NSFrame
{
	public static class ResSystem {
		private static Dictionary<string, Object> _resourceCache = new();

		public static T Load<T>(string path) where T : Object {
			if (_resourceCache.TryGetValue(path, out var obj)) {
				return obj as T;
			}
			T resource = Resources.Load<T>(path);
			if (resource == null) {
				Debug.LogError($"NS: Cannot load asset from path: {path}");
				return null;
			}
			_resourceCache[path] = resource;
			return resource;
		}

		public static void LoadAsync<T>(string path, UnityAction<T> onComplete = null) where T : Object {
			if (_resourceCache.TryGetValue(path, out var obj)) {
				onComplete?.Invoke(obj as T);
				return;
			}
			var request = Resources.LoadAsync<T>(path);
			request.completed += (op) => {
				if (request.asset == null) {
					Debug.LogError($"NS: Cannot load asset from path: {path}");
					onComplete?.Invoke(null);
					return;
				}
				_resourceCache[path] = request.asset;
				onComplete?.Invoke(request.asset as T);
			};
		}

		public static void Unload(string path) {
			if (_resourceCache.TryGetValue(path, out var obj)) {
				Resources.UnloadAsset(obj);
				_resourceCache.Remove(path);
				return;
			}
			Debug.LogWarning($"NS: The resources have not been loaded.");
		}

		public static void ClearCache() {
			foreach (var res in _resourceCache.Values) {
				Resources.UnloadAsset(res);
			}
			_resourceCache.Clear();
		}
	}
}

/* 似乎还不能用，引用计数的问题还没有解决，目前问题在于通过代码看到的资源没有释放，通过 unity profiler 看到的是资源释放了 qwq
namespace NSFrame {	
	using static MonoServiceTool;

	public static class ResSystem {

		public static void LoadPrefabAsync(string assetID, Action<GameObject> callback = null, bool recycle = true) {
			NS_StartCoroutine(DoLoadPrefabAsync(assetID, callback, recycle));
		}
		static IEnumerator DoLoadPrefabAsync(string assetID, Action<GameObject> callback, bool recycle) {
			AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetID);
			yield return handle;

			if (handle.Status != AsyncOperationStatus.Succeeded)
				Debug.LogError($"NS: Failed to load GameObject from address: {assetID}");
			callback?.Invoke(handle.Result);
			if (recycle) Addressables.Release(handle);
		}
		
		public static void InstantiateAsync(string assetID, Transform father = null, Action<GameObject> callback = null) {
			NS_StartCoroutine(DoInstantiateAsync(assetID, father, callback));
		}
		static IEnumerator DoInstantiateAsync(string assetID, Transform father, Action<GameObject> callback) {
			AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetID, father);
			yield return handle;

			if (handle.Status == AsyncOperationStatus.Succeeded) 
				handle.Result.name = handle.Result.name.Replace("(Clone)", string.Empty);
			else Debug.LogError($"NS: Failed to load GameObject from address: {assetID}");
			callback?.Invoke(handle.Result);
		}

		public static void LoadAssetAsync<T>(string assetID, Action<T> callback = null, bool recycle = true) where T : UnityEngine.Object {
			// await Task.Run( () => { DoLoadAssetAsync(assetID, callback, recycle); } );
			NS_StartCoroutine(DoLoadAssetAsync(assetID, callback, recycle));
		}
		static IEnumerator DoLoadAssetAsync<T>(string assetID, Action<T> callback, bool recycle) where T : UnityEngine.Object {
			AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetID);
			yield return handle;

			if (handle.Status == AsyncOperationStatus.Succeeded) 
				callback?.Invoke(handle.Result);
			else {
				Debug.LogError($"NS: Failed to load GameObject from address: {assetID}");
				callback?.Invoke(null);
			}
			if (recycle) Addressables.Release(handle);
		}

		public static void ReleaseAsset<T>(T asset) where T : UnityEngine.Object {
			Addressables.Release(asset);
		}
		public static void ReleaseInstance(GameObject instance) {
			Addressables.ReleaseInstance(instance);
		}
	}
}
*/