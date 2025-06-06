using GameLogic.Utilities;

namespace GameLogic
{
	public sealed class DisasterMgr : ISaveable<DisasterMgrSave>, IMananger {
		private DisasterMgr() {}
		public static DisasterMgr Inst { get; } = new();

		public void ClearMgr() { }

		public DisasterMgrSave GetSave() {
			return new DisasterMgrSave();
		}

		public void InitFromSave(DisasterMgrSave save) {
		}
	}
}