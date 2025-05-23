using GameLogic.Model.Element.Arch;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill
{
	public class WorkTask : TaskBase {
		public override TaskType TaskType => TaskType.Work;

		private ulong _workArchId;
		private ArchLogicBase _workArch;
		private ArchLogicBase WorkArch {
			get => _workArch ??= WorldMgr.Inst.FindArch(_workArchId);
			set => _workArch = value;
		}

		public override void TaskEnd() {
			WorkArch.VillLeave(AttachedVill.ID);
		}
		public override void TaskEnter() {
			WorkArch.VillArrive(AttachedVill.ID);
		}
		public override void TaskExecute() {
			// 通知资源中心产出资源
			if (RepoMgr.Inst.TryVillCons(
					WorkArch.Lconfig.ExtraConsVelsPerOne,
					WorkArch.ConsBuffs_F,
					AttachedVill.ConsBuffs_F
				)) {
				RepoMgr.Inst.VillProd(
					WorkArch.Lconfig.ExtraProdVelsPerOne,
					WorkArch.ProdBuffs_F,
					AttachedVill.ProdBuffs_F
				);
				AttachedVill.AddExp(WorkArch.Lconfig.ExpAdds);
			}
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