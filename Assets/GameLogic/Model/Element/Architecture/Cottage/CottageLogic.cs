namespace GameLogic
{
	public class CottageLogic : ArchLogicBase {
		public override ArchType ArchType => ArchType.Cottage;

		protected override void DerivedInitFromSave(ArchSaveBase save) {}
		protected override ArchSaveBase GetDerivedSave() {
			return new CottageSave();
		}
	}
}