using System.Collections.Generic;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 黑板类，行为树数据共享区。
	/// </summary>
	public class Blackboard : IBlackboard {
		/// <summary>
		/// 存储数据的字典。
		/// </summary>
		private readonly Dictionary<string, object> _datas = new();

		/// <summary>
		/// 获取数据。
		/// 如果数据不存在，则返回null。
		/// </summary>
		/// <param name="key">数据键名</param>
		public object GetData(string key) {
			_datas.TryGetValue(key, out var value);
			return value;
		}

		/// <summary>
		/// 获取指定类型的数据。
		/// 如果数据不存在或类型不匹配，则返回默认值。
		/// </summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="key">数据键名</param>
		public T GetData<T>(string key) {
			if (_datas.TryGetValue(key, out var value) && value is T typedValue) {
				return typedValue;
			}
			return default;
		}

		public void SetData(string key, object value) {
			_datas[key] = value;
		}

		public void RemoveData(string key) {
			_datas.Remove(key);
		}

		public void Clear() {
			_datas.Clear();
		}
	}
}
