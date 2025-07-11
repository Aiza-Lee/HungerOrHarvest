using GameLogic.Features.ClearWorld;

namespace GameLogic.Common.Logic {
	/// <summary>
	/// GidMgr 用于生成全局唯一的 GID（全局唯一标识符）。
	/// GID 是一个递增的无符号长整型，从 1 开始。0 代表无效的 GID。
	/// </summary>
	public class GidMgr : IWorldClearRespondable {
		private static GidMgr _inst;
		public static GidMgr Inst {
			get {
				if (_inst == null) {
					_inst = new GidMgr();
					WorldClearRegistry.Inst.Register(_inst);
				}
				return _inst;
			}
			set {
				if (_inst != null) {
					WorldClearRegistry.Inst.Unregister(_inst);
				}
				_inst = value;
				if (_inst != null) {
					WorldClearRegistry.Inst.Register(_inst);
				}
			}
		}

		private ulong _gid = 1;

		public ulong GetGid() {
			return _gid++;
		}

		public void RespondWorldClear() {
			_gid = 1;
		}
	}
}