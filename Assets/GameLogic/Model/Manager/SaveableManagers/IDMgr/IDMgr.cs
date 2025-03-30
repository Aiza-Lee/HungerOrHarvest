namespace GameLogic
{
	/// <summary>
	/// <para>ID管理器，用于生成唯一ID</para>
	/// <para>ID从1开始，每次调用GetID()方法时，返回当前ID并将ID加1</para>
	/// ID为0表示无效ID
	/// </summary>
	public class IDMgr : ISaveable<IDMgrSave>, IClearMgr {
		private IDMgr() {}
		private static readonly IDMgr _inst = new();
		public static IDMgr Inst => _inst;

		private ulong _curID = 1;

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

		public void ClearMgr() { }
	}
}