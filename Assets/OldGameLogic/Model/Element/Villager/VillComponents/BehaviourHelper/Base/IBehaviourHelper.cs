using NSFrame.BehaviourTree;

namespace OldGameLogic.Model.Element.Vill {
	public interface IBehaviourHelper {
		/// <summary>
		/// 执行行为帮助器的逻辑
		/// </summary>
		void Think();

		/// <summary>
		/// 重置行为树的状态
		/// </summary>
		void Reset();

		/// <summary>
		/// 总结性地描述村民的当前状态
		/// </summary>
		string CurStateDescription { get; }
	}
}