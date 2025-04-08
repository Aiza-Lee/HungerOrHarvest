using System.Collections.Generic;
using GameLogic.View;

namespace GameLogic.Controller
{
	public class LoadGameCmd : CommandBase {
		public LoadGameCmd(List<string> args) : base(args) { }

		public override int ArgCount => 0;

		public override string CmdTitle => "加载存档";
		public override string Description => "加载当前saveInfo的存档";
		public override string FailReason => "";

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			// note: ViewMgr先加载, 因为ModelMgr加载时会触发ViewMgr的事件
			GameViewMgr.Inst.LoadGame();
			GameModelMgr.Inst.LoadGame();
		}
	}
}