namespace GameLogic 
{
	public class WorkSta : StaBase { 
		public override StaType StaType { get => StaType.Work; }

		public override void Enter() {
			throw new System.NotImplementedException();
		}

		public override void Execute() {
			throw new System.NotImplementedException();
		}

		public override void Exit() {
			throw new System.NotImplementedException();
		}

		protected override void DerivedDestroyForPool() {
			throw new System.NotImplementedException();
		}

		protected override void DerivedInitForPool() {
			throw new System.NotImplementedException();
		}

		protected override StaSaveBase GetDerivedSave() {
			throw new System.NotImplementedException();
		}

		protected override void InitDerivedFromSave(StaSaveBase save) {
			throw new System.NotImplementedException();
		}
	}
}