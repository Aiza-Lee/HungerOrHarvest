namespace GameLogic
{
	[System.Serializable]
	public abstract class StaSaveBase {
		public StaType StaType;
		public bool Entered;

		protected abstract StaSaveBase GetDerivedClone();
		public StaSaveBase Clone() {
			var save = GetDerivedClone();
				save.StaType = StaType;
				save.Entered = Entered;
			return save;
		}
	}
}