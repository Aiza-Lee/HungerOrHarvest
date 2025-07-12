namespace GameLogic.Features.SaveLoadData {
	/// <summary>
	/// 实现该接口的类可以响应世界清除事件。
	/// </summary>
	public interface IWorldClearRespondable {
		/// <summary>
		/// 响应世界清除
		/// </summary>
		public void RespondWorldClear();
	}
}