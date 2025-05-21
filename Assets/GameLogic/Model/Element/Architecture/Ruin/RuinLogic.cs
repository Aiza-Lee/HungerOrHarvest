namespace GameLogic.Model.Element.Arch
{
	public class RuinLogic : ArchLogicBase {
		public override ArchType ArchType => ArchType.Ruin;

		protected override void Destroy_Derived() {}

		protected override ArchSaveBase GetSave_Derived() {
			return new RuinSave();
		}

		protected override void InitFromSave_Derived(ArchSaveBase _) {}
	}
}