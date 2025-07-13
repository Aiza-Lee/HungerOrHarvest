namespace GameLogic.Features.WorldDataManager {
	/// <summary>
	/// 作为对框架saveinfo的扩展。
	/// 这个类可以存储一些额外的信息，比如是否是自动保存
	/// </summary>
	public class ExtendSaveInfo {
		/// <summary>
		/// 是否是自动保存
		/// </summary>
		public bool IsAutoSave;

		/// <summary>
		/// 保存的天数
		/// </summary>
		public ulong SaveDay;

		public ExtendSaveInfo(bool isAutoSave, ulong saveDay) {
			IsAutoSave = isAutoSave;
			SaveDay = saveDay;
		}
	}
}