namespace GameLogic
{
	[System.Serializable]
	public class NormalVillSave : VillSaveBase {
		protected override VillSaveBase GetDerivedClone() {
			return new NormalVillSave();
		}
	}
}