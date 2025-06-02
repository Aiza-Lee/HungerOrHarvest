using GameLogic.Model.Element.Arch;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// vill工作的任务，在任务开始（结束）的时候进入（离开）工作arch
	/// <para>task执行期间执行repo产出</para>
	/// </summary>
	public class WorkTask : TaskBase {
		public override TaskType TaskType => TaskType.Work;

		private ArchLogicBase _workArch;
		private ArchLogicBase WorkArch {
			get => _workArch ??= WorldMgr.Inst.FindArch(Impler.BondArchHelper.BondedWorkArchID);
		}

		public override void TaskEnd() {
			WorkArch.VillLeave(Impler.ID);
		}
		public override void TaskEnter() {
			WorkArch.VillArrive(Impler.ID);
		}
		public override void TaskExecute() {
			// 通知资源中心产出资源
			if (RepoMgr.Inst.TryCons(
				WorkArch.Lconfig.ExtraConsVelsPerOne,
				WorkArch.ConsBuffs_F,
				Impler.RepoBuffHelper.ConsBuffs_F
			)) {
				RepoMgr.Inst.Prod(
					WorkArch.Lconfig.ExtraProdVelsPerOne,
					WorkArch.ProdBuffs_F,
					Impler.RepoBuffHelper.ProdBuffs_F
				);
				Impler.ExpHelper.AddExp(WorkArch.Lconfig.ExpAdds);
				Impler.VitHelper.TryConsVit(WorkArch.Lconfig.VitConsRate);
			}
		}

		protected override void InitAfterPop_Derived() { }
		protected override void CleanBeforePush_Derived() { _workArch = null; }

		protected override TaskSaveBase GetSave_Derived() {
			return new WorkTaskSave();
		}
		protected override void InitFromSave_Derived(TaskSaveBase _) { }
	}
}