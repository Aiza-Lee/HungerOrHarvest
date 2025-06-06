namespace GameLogic.Utilities {
	public interface ISaveable<T> {
		/// <summary>
		/// 返回值应该是原始数据的clone
		/// </summary>
		T GetSave();
		/// <summary>
		/// 获得参数save的所有权
		/// </summary>
		void InitFromSave(T save);
	}

}