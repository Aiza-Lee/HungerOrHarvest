using OldGameLogic.View;

namespace OldGameLogic.Controller
{
	/// <summary>
	/// 外部配置控件是否可以操作的统一管理类
	/// </summary>
	public sealed class PlayerControllMgr {
		private PlayerControllMgr() {}
		public static PlayerControllMgr Inst { get; } = new();


		public void SetWorldMainControll(bool controllable) {
			WorldCameraMgr.Inst.Controllable = controllable;
			WorldViewMgr.Inst.Controllable = controllable;
		}
	}
}