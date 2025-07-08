using NsEcsFrame.Core;
using NSFrame.BehaviourTree;

namespace GameLogic.Features.Vill {
	public class VillBehaviourTreeComponent : IComponent {
		public BehaviourTree<VillAiBlackboard> BehaviourTree { get; set; }
	}

	public class VillAiBlackboard : IBlackboard {
		public void Clear() {}
	}
}