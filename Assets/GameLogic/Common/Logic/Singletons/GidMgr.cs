namespace GameLogic.Common.Logic {
	/// <summary>
	/// GidMgr 用于生成全局唯一的 GID（全局唯一标识符）。
	/// GID 是一个递增的无符号长整型，从 1 开始。0 代表无效的 GID。
	/// </summary>
	public class GidMgr {
		public static GidMgr Inst = new();

		private ulong _gid = 1;

		public ulong GetGid() {
			return _gid++;
		}
	}
}