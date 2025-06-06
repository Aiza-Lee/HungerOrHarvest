namespace GameLogic.Model.Element.Arch {
	[System.Serializable]
	public class HunterCabinSave : ArchSaveBase {
		public override ArchType ArchType => ArchType.HunterCabin;

		protected override ArchSaveBase GetDerivedClone() {
			return new HunterCabinSave();
		}
	}
}