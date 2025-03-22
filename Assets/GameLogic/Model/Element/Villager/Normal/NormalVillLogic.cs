namespace GameLogic
{
	public class NormalVillLogic : VillLogicBase {
		public override VillType VillType => VillType.Normal;

		protected override void DerivedInitFromSave(VillSaveBase save) {}

		protected override VillSaveBase GetDerivedSave() {
			return new NormalVillSave();
		}
	}
}