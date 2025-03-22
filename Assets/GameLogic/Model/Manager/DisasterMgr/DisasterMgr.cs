namespace GameLogic
{
	public sealed class DisasterMgr : ISaveable<DisasterMgrSave> {
		private DisasterMgr() {}
		public static DisasterMgr Inst { get; } = new();


		public DisasterMgrSave GetSave() {
			return new DisasterMgrSave();
		}

		public void InitFromSave(DisasterMgrSave save) {
		}
	}
}