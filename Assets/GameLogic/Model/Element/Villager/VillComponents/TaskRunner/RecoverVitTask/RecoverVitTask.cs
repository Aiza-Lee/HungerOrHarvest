using GameLogic.Model.Element.Arch;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// vill回家回复体力的任务
	/// <para></para>
	/// </summary>
	public class RecoverVitTask : TaskBase {
		public override TaskType TaskType => TaskType.Eat;

		private CottageLogic _cottage;
		private CottageLogic Cottage {
			get => _cottage ??= WorldMgr.Inst.FindArch(Impler.BondArchHelper.HomeID) as CottageLogic;
		}

		public override void TaskEnd() {
			Cottage.VillLeave(Impler.ID);
		}

		public override void TaskEnter() {
			Cottage.VillArrive(Impler.ID);
		}

		public override void TaskExecute() {
			var cfg = ConfigMgr.Config.VitConfig;
			var vitHelper = Impler.VitHelper;
			if (vitHelper.VitPercentage >= cfg.RecoveryVitThreshold) {
				TaskEnd();
				return;
			}

			if (RepoMgr.Inst.TryCons(new(new RTPair<float>(RepoType.Food, cfg.FoodConsRate)), Impler.RepoBuffHelper.ConsBuffs_F)) {
				vitHelper.AddVit(cfg.FoodConsRate * cfg.FoodToVitRatio);
			} else {
				TaskEnd();
			}
		}

		protected override void CleanBeforePush_Derived() { _cottage = null; }
		protected override void InitAfterPop_Derived() { }

		protected override TaskSaveBase GetSave_Derived() {
			return new RecoverVitTaskSave();
		}
		protected override void InitFromSave_Derived(TaskSaveBase save) { }
	}
}