using System;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 装饰节点基类，只有一个子节点，用于修改子节点行为。
	/// </summary>
	public abstract class DecoratorNode : BehaviourNode {
		/// <summary>
		/// 被装饰的子节点。
		/// </summary>
		protected BehaviourNode _child;
		/// <summary>
		/// 设置子节点。
		/// </summary>
		/// <param name="child">要设置的子节点</param>
		public void SetChild(BehaviourNode child) {
			if (_child != null) {
				throw new InvalidOperationException($"装饰节点 {this.GetType().Name} 只能有一个子节点。");
			}
			_child = child;
			if (child != null) child.Parent = this;
		}
	}

	/// <summary>
	/// Inverter节点：将子节点的SUCCESS/FAILURE结果取反。
	/// </summary>
	public class Inverter : DecoratorNode {
		public override NodeStatus Execute() {
			if (_child == null) return NodeStatus.FAILURE;
			var status = _child.Execute();
			if (status == NodeStatus.SUCCESS) Status = NodeStatus.FAILURE;
			else if (status == NodeStatus.FAILURE) Status = NodeStatus.SUCCESS;
			else Status = NodeStatus.RUNNING;
			return Status;
		}
	}

	/// <summary>
	/// Repeater节点：重复执行子节点指定次数。
	/// </summary>
	public class Repeater : DecoratorNode {
		/// <summary>
		/// 重复次数。
		/// </summary>
		public int RepeatCount { get; set; }
		private int _curCount = 0;
		public override NodeStatus Execute() {
			if (_child == null) return NodeStatus.FAILURE;
			while (_curCount < RepeatCount) {
				var status = _child.Execute();
				if (status == NodeStatus.RUNNING) {
					Status = NodeStatus.RUNNING;
					return Status;
				}
				_curCount++;
			}
			Status = NodeStatus.SUCCESS;
			_curCount = 0;
			return Status;
		}
		/// <summary>
		/// 重置计数。
		/// </summary>
		public override void Reset() { _curCount = 0; base.Reset(); }
	}
}
