using System;

namespace NSFrame.BehaviourTree {
	/// <summary>
	/// 行为树主类，管理根节点和黑板，负责整体决策流程。
	/// <para>
	/// <b>使用BehaviourTreeBuilder构建行为树：</b>
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
	/// </para>
	/// </summary>
	public class BehaviourTree<BBT> where BBT : IBlackboard {
		/// <summary>
		/// 行为树的根节点。
		/// </summary>
		private readonly BehaviourNode _root;
		/// <summary>
		/// 黑板，存储共享数据。
		/// </summary>
		private readonly BBT _blackboard;

		private readonly Action<BBT> _finalAction;

		public BehaviourTree(BehaviourNode root, BBT blackboard, Action<BBT> finalAction = null) {
			_root = root ?? throw new ArgumentNullException(nameof(root), "行为树的根节点不能为空。");
			_blackboard = blackboard ?? throw new ArgumentNullException(nameof(blackboard), "黑板对象不能为空。");
			_finalAction = finalAction;
		}

		/// <summary>
		/// 执行一次决策（从根节点递归执行）。
		/// </summary>
		/// <returns>根节点的执行状态（SUCCESS/FAILURE/RUNNING）</returns>
		public NodeStatus? Think() {
			var result = _root?.Execute();
			_finalAction?.Invoke(_blackboard);
			return result;
		}
		/// <summary>
		/// 重置整棵树的状态。
		/// </summary>
		public void Reset() {
			_root?.Reset();
		}
	}
}
