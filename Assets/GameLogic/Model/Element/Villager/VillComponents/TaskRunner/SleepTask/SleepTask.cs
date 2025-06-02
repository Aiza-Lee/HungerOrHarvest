using GameLogic.Model.Element.Arch;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// vill睡觉任务，在任务开始（结束）的时候进入（离开）home
	/// <para>任务执行期间，会不断尝试消耗食物并回复体力</para>
	/// </summary>
	public class SleepTask : TaskBase {
		public override TaskType TaskType => TaskType.Sleep;
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
			if (vitHelper.VitPercentage >= 1f) return;

			if (RepoMgr.Inst.TryCons(new(new RTPair<float>(RepoType.Food, cfg.FoodConsRate)), Impler.RepoBuffHelper.ConsBuffs_F)) {
				vitHelper.AddVit(cfg.FoodConsRate * cfg.FoodToVitRatio);
			}
		}
		protected override void CleanBeforePush_Derived() { _cottage = null; }
		protected override void InitAfterPop_Derived() { }


		protected override TaskSaveBase GetSave_Derived() {
			return new SleepTaskSave();
		}
		protected override void InitFromSave_Derived(TaskSaveBase _) { }
	}
}