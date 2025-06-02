using GameLogic.Model.Element.Arch;

namespace GameLogic.Model.Element.Vill {
	public interface IVillTaskRun {
		/// <summary>
		/// 当前任务类型
		/// </summary>
		TaskType? CurTaskType { get; }
		/// <summary>
		/// 当前移动到目标类型
		/// </summary>
		MoveToTargetType? CurMoveToTargetType { get; }
		/// <summary>
		/// 直接触发当前任务的End，并将所有任务列表替换为传入的任务
		/// </summary>
		void ResetTasks(params TaskBase[] task);
		bool SetGoWorkTasks(ArchLogicBase arch);
		bool SetGoSleepTasks();
		bool SetGoRecoverVitTasks();
	}
}