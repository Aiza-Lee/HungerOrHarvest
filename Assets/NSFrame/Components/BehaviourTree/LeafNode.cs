using System;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 叶节点基类，实际执行动作或条件判断。
	/// </summary>
	public abstract class LeafNode<BBT> : BehaviourNode where BBT : IBlackboard {
		/// <summary>
		/// 黑板对象，存储共享数据。
		/// </summary>
		protected BBT _blackboard;

		/// <summary>
		/// 构造函数，传入黑板对象。
		/// </summary>
		public LeafNode(BBT blackboard) {
			_blackboard = blackboard;
		}

		/// <summary>
		/// 重置节点状态，并清除黑板数据。
		/// </summary>
		public override void Reset() {
			base.Reset();
			_blackboard?.Clear();
		}
	}

	/// <summary>
	/// 动作节点：执行具体行为。
	/// </summary>
	public class ActionNode<BBT> : LeafNode<BBT> where BBT : IBlackboard {
		private readonly Func<BBT, NodeStatus> _action;
		/// <summary>
		/// 构造函数，传入行为委托。
		/// </summary>
		/// <param name="action">行为委托</param>
		public ActionNode(Func<BBT, NodeStatus> action, BBT blackboard) : base(blackboard) {
			this._action = action;
		}
		/// <summary>
		/// 执行动作，返回状态。
		/// </summary>
		/// <returns>节点执行后的状态</returns>
		public override NodeStatus Execute() {
			Status = _action?.Invoke(_blackboard) ?? NodeStatus.FAILURE;
			return Status;
		}
	}

	/// <summary>
	/// 条件节点：判断条件，通常返回SUCCESS/FAILURE。
	/// </summary>
	public class ConditionNode<BBT> : LeafNode<BBT> where BBT : IBlackboard {
		private readonly Func<BBT, bool> _condition;
		/// <summary>
		/// 构造函数，传入条件委托和黑板。
		/// </summary>
		/// <param name="condition">条件委托</param>
		/// <param name="blackboard">黑板对象</param>
		public ConditionNode(Func<BBT, bool> condition, BBT blackboard) : base(blackboard) {
			this._condition = condition;
		}
		/// <summary>
		/// 执行条件判断，返回状态。
		/// </summary>
		/// <returns>节点执行后的状态</returns>
		public override NodeStatus Execute() {
			Status = (_condition != null && _condition(_blackboard)) ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
			return Status;
		}
	}
}
