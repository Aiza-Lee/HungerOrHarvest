namespace GameLogic
{
	public class SleepTask : TaskBase {
		public override TaskType TaskType => TaskType.Sleep;

		private ulong _homeID;
		private CottageLogic _cottage;
		private CottageLogic Cottage {
			get => _cottage ??= WorldMgr.Inst.FindArch(_homeID) as CottageLogic;
			set => _cottage = value;
		}

		public override void End() {
			Cottage.VillLeave(AttachedVill.ID);
		}

		public override void Enter() {
			Cottage.VillArrive(AttachedVill.ID);
		}

		public override void Execute() {}

		protected override void CleanBeforePush_Derived() { Cottage = null; }
		protected override void InitAfterPop_Derived() { }


		protected override TaskSaveBase GetSave_Derived() {
			return new SleepTaskSave() {
				HomeID = _homeID
			};
		}
		protected override void InitFromSave_Derived(TaskSaveBase save) {
			var sv = save as SleepTaskSave;
			_homeID = sv.HomeID;
		}
	}
}