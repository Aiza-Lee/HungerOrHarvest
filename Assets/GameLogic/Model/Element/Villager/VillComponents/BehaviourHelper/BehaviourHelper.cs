using NSFrame.BehaviourTree;

namespace GameLogic.Model.Element.Vill {
	public class BehaviourHelper : IBehaviourHelper {

		private readonly BehaviourTree<MyBlackboard> _behaviourTree;
		public BehaviourHelper(BehaviourTree<MyBlackboard> behaviourTree) {
			_behaviourTree = behaviourTree;
		}

		public virtual NodeStatus? Think() {
			return null;
		}

		public virtual void Reset() {
			// 默认实现为空
		}
	}
}