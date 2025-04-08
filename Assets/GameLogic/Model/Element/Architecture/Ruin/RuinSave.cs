namespace GameLogic
{
	[System.Serializable]
	public class RuinSave : ArchSaveBase {
		protected override ArchSaveBase GetDerivedClone() {
			return new RuinSave();
		}

	}
}