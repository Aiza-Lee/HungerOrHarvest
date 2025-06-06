namespace GameLogic.Model.Element.Vill {
	public enum MoveToTargetType {
		/// <summary>
		/// 前往工作地点
		/// </summary>
		Work,
		
		/// <summary>
		/// 前往休息地点
		/// </summary>
		Sleep,

		/// <summary>
		/// 前往随机地点
		/// </summary>
		Random,

		/// <summary>
		/// 前往低体力地点(Home)
		/// </summary>
		Recover,

		/// <summary>
		/// 前往死亡地点
		/// </summary>
		Die,
	}
}