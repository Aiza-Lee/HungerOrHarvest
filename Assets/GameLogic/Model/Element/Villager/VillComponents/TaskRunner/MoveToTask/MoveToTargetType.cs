namespace GameLogic.Model.Element.Vill
{
	/// <summary>
	/// MoveTo Task的目标类型
	/// </summary>
	public enum MoveToTargetType {
		/// <summary>
		/// 目标为随机游走的空地
		/// </summary>
		Spare,
		/// <summary>
		/// 目标为某个工作的建筑
		/// </summary>
		WorkArch,
		/// <summary>
		/// 目标为家，且是前去睡觉
		/// </summary>
		HomeSleep,
		/// <summary>
		/// 目标为家，且是前去吃饭
		/// </summary>
		HomeEat,
		/// <summary>
		/// 目标为离开村庄
		/// </summary>
		Outer,
	}
}