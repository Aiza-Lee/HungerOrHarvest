using System.Collections.Generic;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 组合节点基类，拥有多个子节点，控制子节点执行顺序。
	/// </summary>
	public abstract class CompositeNode : BehaviourNode {
		/// <summary>
		/// 子节点列表。
		/// </summary>
		protected List<BehaviourNode> Children = new();
		/// <summary>
		/// 添加子节点。
		/// </summary>
		/// <param name="child">要添加的子节点</param>
		public void AddChild(BehaviourNode child) {
			child.Parent = this;
			Children.Add(child);
		}
	}

	/// <summary>
	/// Selector节点：依次执行子节点，遇到SUCCESS或RUNNING即返回。
	/// </summary>
	public class Selector : CompositeNode {
		public override NodeStatus Execute() {
			foreach (var child in Children) {
				var childResult = child.Execute();
				if (childResult == NodeStatus.SUCCESS || childResult == NodeStatus.RUNNING) {
					Status = childResult;
					return childResult;
				}
			}
			Status = NodeStatus.FAILURE;
			return Status;
		}
	}

	/// <summary>
	/// Sequence节点：依次执行子节点，遇到FAILURE或RUNNING即返回。
	/// </summary>
	public class Sequence : CompositeNode {
		public override NodeStatus Execute() {
			foreach (var child in Children) {
				var childResult = child.Execute();
				if (childResult == NodeStatus.FAILURE || childResult == NodeStatus.RUNNING) {
					Status = childResult;
					return childResult;
				}
			}
			Status = NodeStatus.SUCCESS;
			return Status;
		}
	}
}
