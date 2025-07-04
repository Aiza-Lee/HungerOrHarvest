namespace OldGameLogic.Model.Element.Arch {
	public class HunterCabinLogic : ArchLogicBase {
		public override ArchType ArchType => ArchType.HunterCabin;

		protected override void Destroy_Derived() {
		}

		protected override ArchSaveBase GetSave_Derived() {
			return new HunterCabinSave();
		}

		protected override void InitFromSave_Derived(ArchSaveBase _) {}
	}
}