namespace OldGameLogic.Model.Element.Arch
{
	public class CottageLogic : ArchLogicBase {
		public override ArchType ArchType => ArchType.Cottage;

		protected override void Destroy_Derived() { }

		protected override void InitFromSave_Derived(ArchSaveBase _) { }
		protected override ArchSaveBase GetSave_Derived() {
			return new CottageSave();
		}
	}
}