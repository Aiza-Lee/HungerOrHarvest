using System.Collections.Generic;

namespace GameLogic.Features.WorldDataManager {
	/// <summary>
	/// 标记一个Resource可以被存档
	/// </summary>
	public interface ISaveableResource {
		/// <summary>
		/// 从已经反序列化的数据中加载数据，遍历实现即可
		/// </summary>
		void Load(IEnumerable<object> loadedData);
	}
}