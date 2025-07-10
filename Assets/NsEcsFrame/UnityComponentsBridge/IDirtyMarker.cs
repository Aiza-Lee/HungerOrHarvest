namespace NsEcsFrame.Components {
	public interface IDirtyMarker {
		/// <summary>
		/// 标记组件为脏
		/// </summary>
		void MarkDirty();

		/// <summary>
		/// 清除脏标记
		/// </summary>
		void ClearDirty();

		/// <summary>
		/// 是否为脏数据
		/// </summary>
		bool IsDirty();
	}
}