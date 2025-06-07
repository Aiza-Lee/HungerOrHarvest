using System;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 叶节点基类，实际执行动作或条件判断。
	/// </summary>
	public abstract class LeafNode : BehaviourNode { }

	/// <summary>
	/// 动作节点：执行具体行为。
	/// </summary>
	public class ActionNode : LeafNode {
		private readonly Func<NodeStatus> _action;
		/// <summary>
		/// 构造函数，传入行为委托。
		/// </summary>
		/// <param name="action">行为委托</param>
		public ActionNode(Func<NodeStatus> action) {
			this._action = action;
		}
		/// <summary>
		/// 执行动作，返回状态。
		/// </summary>
		/// <returns>节点执行后的状态</returns>
		public override NodeStatus Execute() {
			Status = _action != null ? _action() : NodeStatus.FAILURE;
			return Status;
		}
	}

	/// <summary>
	/// 条件节点：判断条件，通常返回SUCCESS/FAILURE。
	/// </summary>
	public class ConditionNode : LeafNode {
		private readonly Func<Blackboard, bool> _condition;
		private readonly Blackboard _blackboard;
		/// <summary>
		/// 构造函数，传入条件委托和黑板。
		/// </summary>
		/// <param name="condition">条件委托</param>
		/// <param name="blackboard">黑板对象</param>
		public ConditionNode(Func<Blackboard, bool> condition, Blackboard blackboard) {
			this._condition = condition;
			this._blackboard = blackboard;
		}
		/// <summary>
		/// 执行条件判断，返回状态。
		/// </summary>
		/// <returns>节点执行后的状态</returns>
		public override NodeStatus Execute() {
			Status = (_condition != null && _blackboard != null && _condition(_blackboard)) ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
			return Status;
		}
	}
}
