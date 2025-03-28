namespace GameLogic
{
	public class WorkTask : TaskBase {
		public override TaskType TaskType => TaskType.Work;

		private ulong _workArchId;
		private ArchLogicBase _workArch;
		private ArchLogicBase WorkArch {
			get => _workArch ??= WorldMgr.Inst.FindArch(_workArchId);
			set => _workArch = value;
		}

		public override void End() {
			WorkArch.VillLeave(AttachedVill);
		}
		public override void Enter() {
			WorkArch.VillArrive(AttachedVill);
		}
		public override void Execute() {
			// note: 似乎村民的资源产出逻辑应该写在这里，目前写在 ArchLogicBase 的 UpdateRepo 中
		}

		protected override void InitAfterPop_Derived() {}
		protected override void CleanBeforePush_Derived() {
			WorkArch = null;
		}

		protected override TaskSaveBase GetSave_Derived() {
			return new WorkTaskSave() {
				WorkArchId = _workArchId,
			};
		}
		protected override void InitFromSave_Derived(TaskSaveBase save) {
			var sv = save as WorkTaskSave;
			_workArchId = sv.WorkArchId;
		}
	}
}