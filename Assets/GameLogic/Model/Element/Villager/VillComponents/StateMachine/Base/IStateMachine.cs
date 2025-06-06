namespace GameLogic.Model.Element.Vill {
	public interface IStateMachine : ILogicDestroy {
		/// <summary>
		/// 当前状态类型
		/// </summary>
		State CurStaType { get; }

		/// <summary>
		/// 每日剩余的恢复次数
		/// </summary>
		int RecoverChance { get; set; }

		/// <summary>
		/// 当前正在前往那里，如果没有移动则为null
		/// </summary>
		MoveToTargetType? MoveToTarget { get; set; }

		/// <summary>
		/// 目标坐标
		/// </summary>
		Coord? MoveTargetCoord { get; set; }

		/// <summary>
		/// 当前状态是否是Dying状态
		/// </summary>
		bool IsDying { get; set; }

		/// <summary>
		/// 综合描述当前状态
		/// </summary>
		string CurStateDescription { get; }
	}
}