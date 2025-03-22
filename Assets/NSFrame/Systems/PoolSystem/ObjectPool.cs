using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace NSFrame {
	public class ObjectPool<T> : IObjectPool where T : class,IPooledObject, new() {

		const int MAX_POP_PER_FRAME = 4;

		private readonly Queue<T> _poolQue;
		private int _maxCapacity;
		
		public int Count => _poolQue.Count;


		public ObjectPool(int maxCapacity = -1) {
			_maxCapacity = maxCapacity;
			_poolQue = new();
		}
		public void Push(object obj) {
			if (obj is not T item) {
				Debug.LogError("NS: Trying to put a wrong type object to ObjectPool.");
				return;
			}
			item.DestroyForPool();
			if (_maxCapacity != -1 && _poolQue.Count == _maxCapacity) return;
			_poolQue.Enqueue(item);
		}
		public object Pop() {
			if (!_poolQue.TryDequeue(out T obj)) {
				obj = new T();
			}
			obj.InitForPool();
			return obj;
		}
		public void SetMaxCapacity(int maxCapacity) {
			_maxCapacity = maxCapacity;
			MonoServiceTool.NS_StartCoroutine(DoPopMoreElement());
		}
		IEnumerator DoPopMoreElement() {
			while (_poolQue.Count > _maxCapacity) {
				int tmp = Mathf.Min(MAX_POP_PER_FRAME, _poolQue.Count - _maxCapacity);
				for (int i = 0; i < tmp; ++i) _poolQue.TryDequeue(out T _);
				yield return null;
			}
		}
	}
}


