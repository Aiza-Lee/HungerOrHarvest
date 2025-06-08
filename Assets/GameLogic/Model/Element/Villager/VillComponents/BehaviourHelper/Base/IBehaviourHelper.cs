using NSFrame.BehaviourTree;

namespace GameLogic.Model.Element.Vill {
	public interface IBehaviourHelper {
		/// <summary>
		/// 执行行为帮助器的逻辑
		/// </summary>
		NodeStatus? Think();

		/// <summary>
		/// 重置行为树的状态
		/// </summary>
		void Reset();
	}
}