namespace GameLogic.Model.Element.Arch
{
	[System.Serializable]
	public class RuinSave : ArchSaveBase {
		public override ArchType ArchType => ArchType.Ruin;

		protected override ArchSaveBase GetDerivedClone() {
			return new RuinSave();
		}

	}
}