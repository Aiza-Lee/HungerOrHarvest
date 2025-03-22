namespace GameLogic
{
	public class IDMgr : ISaveable<IDMgrSave> {
		private IDMgr() {}
		private static readonly IDMgr _inst = new();
		public static IDMgr Inst => _inst;

		private ulong _curID;

		public ulong GetID() {
			return _curID++;
		}

		public IDMgrSave GetSave() {
			return new IDMgrSave() {
				CurID = _curID,
			};
		}

		public void InitFromSave(IDMgrSave save) {
			_curID = save.CurID;
		}
	}
}