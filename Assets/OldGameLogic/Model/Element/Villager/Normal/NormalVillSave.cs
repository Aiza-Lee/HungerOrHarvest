namespace OldGameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class NormalVillSave : VillSaveBase {
		public override VillType VillType => VillType.Normal;

		protected override VillSaveBase GetDerivedClone() {
			return new NormalVillSave();
		}
	}
}