using NsEcsFrame.Core;
using NSFrame.BehaviourTree;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// VillBehaviourTreeComponent 用于存储村民的行为树对象
	/// </summary>
	public class VillBehaviourTreeComponent : IComponent {
		public BehaviourTree<VillAiBlackboard> BehaviourTree { get; set; }
	}

	/// <summary>
	/// VillAiBlackboard 用于存储村民行为树的黑板数据
	/// </summary>
	public class VillAiBlackboard : IBlackboard {
		public void Clear() { }
	}
}