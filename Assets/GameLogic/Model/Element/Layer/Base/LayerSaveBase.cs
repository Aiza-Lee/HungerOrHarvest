namespace GameLogic
{
	[System.Serializable]
	public abstract class LayerSaveBase {
		public ulong ID;
		public LayerType LayerType;
		public int LYR;
		
		protected abstract LayerSaveBase GetDerivedClone();
		public LayerSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID = ID;
				save.LayerType = LayerType;
				save.LYR = LYR;
			return save;
		}
	}
}