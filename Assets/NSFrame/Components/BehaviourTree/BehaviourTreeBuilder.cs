using System;
using System.Collections.Generic;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 行为树构建器，支持链式API快速搭建行为树结构。
	/// <para>使用示例：</para>
	/// <code>
	/// var builder = new BehaviourTreeBuilder();
	/// var tree = builder
	///     .Selector()
	///         .Sequence()
	///             .Condition(bb => bb.GetData("HasTarget") != null, blackboard)
	///             .Action(() => NodeStatus.SUCCESS)
	///         .End()
	///         .Action(() => NodeStatus.FAILURE)
	///         .CustomLeaf(new BlackboardConditionNode("Health", v => (int)v > 50, blackboard))
	///         .Inverter()
	///             .Action(...)
	///         .End()
	///         .Repeater(3)
	///             .Action(...)
	///         .End()
	///         .CustomDecorator(new MyCustomDecorator(...))
	///             .Action(...)
	///         .End()
	///     .End()
	///     .Build();
	/// </code>
	/// <b>Builder支持：</b>
	/// <list type="bullet">
	/// <item>内置节点类型：Selector、Sequence、ActionNode、ConditionNode、Inverter、Repeater 等，均有专用链式方法。</item>
	/// <item>自定义叶子节点：通过 <c>CustomLeaf(LeafNode node)</c> 方法支持任意LeafNode派生节点（如 BlackboardConditionNode、WaitNode 等）。</item>
	/// <item>自定义装饰节点：通过 <c>CustomDecorator(DecoratorNode node)</c> 方法支持任意DecoratorNode派生节点。</item>
	/// <item>链式API可嵌套调用，End() 返回上一级节点，Build() 构建最终行为树。</item>
	/// </list>
	/// <b>说明：</b>
	/// <para>行为树每次调用 Think() 时，从根节点递归执行，返回根节点的执行状态（SUCCESS/FAILURE/RUNNING）。
	/// 可通过 Reset() 重置整棵树的状态。黑板（Blackboard）用于节点间数据共享。</para>
	/// </summary>
	public class BehaviourTreeBuilder {
		private readonly Stack<BehaviourNode> _nodeStack = new();
		private BehaviourNode _root;

		/// <summary>
		/// 开始一个Selector节点。
		/// </summary>
		public BehaviourTreeBuilder Selector() {
			var node = new Selector();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 开始一个Sequence节点。
		/// </summary>
		public BehaviourTreeBuilder Sequence() {
			var node = new Sequence();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 添加一个Action节点。
		/// </summary>
		public BehaviourTreeBuilder Action(Func<NodeStatus> action) {
			var node = new ActionNode(action);
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个Condition节点。
		/// </summary>
		public BehaviourTreeBuilder Condition(Func<Blackboard, bool> condition, Blackboard blackboard) {
			var node = new ConditionNode(condition, blackboard);
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个自定义叶子节点（仅支持LeafNode派生类型）。
		/// </summary>
		/// <param name="node">自定义叶子节点实例</param>
		public BehaviourTreeBuilder CustomLeaf(LeafNode node) {
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个自定义装饰节点（仅支持DecoratorNode派生类型），并将其作为当前节点，支持链式嵌套。
		/// 仅允许添加一个子节点，需配合End()闭合。
		/// </summary>
		/// <param name="decorator">自定义装饰节点实例</param>
		public BehaviourTreeBuilder CustomDecorator(DecoratorNode decorator) {
			AddNode(decorator);
			_nodeStack.Push(decorator);
			return this;
		}

		/// <summary>
		/// 添加一个Inverter装饰节点，并将其作为当前节点，支持链式嵌套。需配合End()闭合。
		/// </summary>
		public BehaviourTreeBuilder Inverter() {
			var node = new Inverter();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 添加一个Repeater装饰节点，并将其作为当前节点，支持链式嵌套。需配合End()闭合。
		/// </summary>
		/// <param name="repeatCount">重复次数</param>
		public BehaviourTreeBuilder Repeater(int repeatCount) {
			var node = new Repeater { RepeatCount = repeatCount };
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 结束当前节点，返回上一级节点。
		/// </summary>
		public BehaviourTreeBuilder End() {
			if (_nodeStack.Count > 0)
				_nodeStack.Pop();
			return this;
		}

		/// <summary>
		/// 构建行为树对象。
		/// </summary>
		public BehaviourTree Build() {
			// 检查所有装饰节点是否有子节点
			if (_root != null) {
				var stack = new Stack<BehaviourNode>();
				stack.Push(_root);
				while (stack.Count > 0) {
					var node = stack.Pop();
					if (node is DecoratorNode decorator) {
						// 通过反射获取子节点
						var childFieldInfo = decorator.GetType().GetField("_child", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
						if (childFieldInfo?.GetValue(decorator) is not BehaviourNode child) {
							throw new InvalidOperationException($"装饰节点 {decorator.GetType().Name} 没有子节点，请检查行为树构建逻辑。");
						}
						stack.Push(child);
					} else if (node is CompositeNode composite) {
						// 通过反射获取子节点集合
						var childrenFieldInfo = composite.GetType().GetField("_children", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
						if (childrenFieldInfo?.GetValue(composite) is IEnumerable<BehaviourNode> children) {
							foreach (var child in children) {
								stack.Push(child);
							}
						}
					}
				}
			}
			return new BehaviourTree { Root = _root };
		}

		private void AddNode(BehaviourNode node) {
			if (_nodeStack.Count == 0) {
				_root = node;
			} else {
				var parent = _nodeStack.Peek();
				if (parent is CompositeNode composite) {
					composite.AddChild(node);
				} else if (parent is DecoratorNode decorator) {
					decorator.SetChild(node);
				}
			}
		}
	}
}