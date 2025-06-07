namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 节点执行状态枚举，表示行为树节点的运行结果。
	/// </summary>
	public enum NodeStatus {
		/// <summary>
		/// 执行成功
		/// </summary>
		SUCCESS,
		/// <summary>
		/// 执行失败
		/// </summary>
		FAILURE,
		/// <summary>
		/// 正在执行
		/// </summary>
		RUNNING
	}
}
