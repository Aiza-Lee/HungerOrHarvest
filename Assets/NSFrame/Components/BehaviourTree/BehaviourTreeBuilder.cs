using System;
using System.Collections.Generic;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 行为树构建器，支持链式API快速搭建行为树结构。
	/// <para>使用示例：</para>
	/// <code>
	/// var builder<BBT> = new BehaviourTreeBuilder<BBT>();
	/// var tree = builder<BBT>
	/// 	.Blackboard(new MyBlackboard())
	///     .Selector()
	///         .Sequence()
	///             .Condition(bb => bb.GetData("HasTarget") != null)
	///             .Action(() => NodeStatus.SUCCESS)
	///         .End()
	///         .Action(() => NodeStatus.FAILURE)
	///         .CustomLeaf(new BlackboardConditionNode("Health", v => (int)v > 50))
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
	/// <b>Builder<BBT>支持：</b>
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
	public class BehaviourTreeBuilder<BBT> where BBT : IBlackboard {
		private readonly Stack<BehaviourNode> _nodeStack = new();
		private BehaviourNode _root;
		private BBT _blackboard;
		private Action<BBT> _finalAction;

		/// <summary>
		/// 设置黑板对象，供行为树节点使用。
		/// <para>黑板用于存储共享数据，供各个节点访问和修改。</para>
		/// </summary>
		/// <param name="blackboard">黑板对象</param>
		public BehaviourTreeBuilder<BBT> Blackboard(BBT blackboard) {
			_blackboard = blackboard;
			return this;
		}

		/// <summary>
		/// 设置最终动作，在行为树执行完毕后调用。
		/// </summary>
		/// <param name="finalAction"></param>
		/// <returns></returns>
		public BehaviourTreeBuilder<BBT> FinalAction(Action<BBT> finalAction) {
			_finalAction = finalAction;
			return this;
		}

		/// <summary>
		/// 开始一个Selector节点。
		/// </summary>
		public BehaviourTreeBuilder<BBT> Selector() {
			var node = new Selector();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 开始一个Sequence节点。
		/// </summary>
		public BehaviourTreeBuilder<BBT> Sequence() {
			var node = new Sequence();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 添加一个Action节点。
		/// </summary>
		public BehaviourTreeBuilder<BBT> Action(Func<BBT, NodeStatus> action) {
			var node = new ActionNode<BBT>(action, _blackboard);
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个Condition节点。
		/// </summary>
		public BehaviourTreeBuilder<BBT> Condition(Func<BBT, bool> condition) {
			var node = new ConditionNode<BBT>(condition, _blackboard);
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个自定义叶子节点（仅支持LeafNode派生类型）。
		/// </summary>
		/// <param name="node">自定义叶子节点实例</param>
		public BehaviourTreeBuilder<BBT> CustomLeaf(LeafNode<BBT> node) {
			AddNode(node);
			return this;
		}

		/// <summary>
		/// 添加一个自定义装饰节点（仅支持DecoratorNode派生类型），并将其作为当前节点，支持链式嵌套。
		/// 仅允许添加一个子节点，需配合End()闭合。
		/// </summary>
		/// <param name="decorator">自定义装饰节点实例</param>
		public BehaviourTreeBuilder<BBT> CustomDecorator(DecoratorNode decorator) {
			AddNode(decorator);
			_nodeStack.Push(decorator);
			return this;
		}

		/// <summary>
		/// 添加一个Inverter装饰节点，并将其作为当前节点，支持链式嵌套。需配合End()闭合。
		/// </summary>
		public BehaviourTreeBuilder<BBT> Inverter() {
			var node = new Inverter();
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 添加一个Repeater装饰节点，并将其作为当前节点，支持链式嵌套。需配合End()闭合。
		/// </summary>
		/// <param name="repeatCount">重复次数</param>
		public BehaviourTreeBuilder<BBT> Repeater(int repeatCount) {
			var node = new Repeater { RepeatCount = repeatCount };
			AddNode(node);
			_nodeStack.Push(node);
			return this;
		}

		/// <summary>
		/// 结束当前节点，返回上一级节点。
		/// </summary>
		public BehaviourTreeBuilder<BBT> End() {
			if (_nodeStack.Count > 0)
				_nodeStack.Pop();
			return this;
		}

		/// <summary>
		/// 构建行为树对象。
		/// </summary>
		public BehaviourTree<BBT> Build() {
			if (_blackboard == null) {
				throw new InvalidOperationException("黑板对象未设置，请调用 Blackboard() 方法设置黑板。");
			}
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
			} else {
				throw new InvalidOperationException("行为树构建失败，根节点未设置。请检查是否调用了 Selector() 或 Sequence() 等方法。");
			}
			return new BehaviourTree<BBT>(_root, _blackboard);
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