namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public abstract class VillSaveBase {
		abstract public VillType VillType { get; }

		public LogicImplerSave LogicImpler;
		
		protected abstract VillSaveBase GetDerivedClone();
		public VillSaveBase Clone() {
			var save = GetDerivedClone();
				save.LogicImpler 	= LogicImpler.Clone();
			return save;
		}
	}
}