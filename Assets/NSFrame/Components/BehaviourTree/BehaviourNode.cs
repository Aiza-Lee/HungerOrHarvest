namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 行为树节点基类，所有节点类型的基础。
	/// </summary>
	public abstract class BehaviourNode {
		/// <summary>
		/// 当前节点的执行状态。
		/// </summary>
		public NodeStatus Status { get; protected set; }
		/// <summary>
		/// 父节点引用。
		/// </summary>
		public BehaviourNode Parent { get; set; }
		/// <summary>
		/// 执行节点逻辑，返回节点状态。
		/// </summary>
		/// <returns>节点执行后的状态（SUCCESS/FAILURE/RUNNING）</returns>
		public abstract NodeStatus Execute();
		/// <summary>
		/// 重置节点状态，默认设为FAILURE。
		/// </summary>
		public virtual void Reset() { Status = NodeStatus.FAILURE; }
	}
}
