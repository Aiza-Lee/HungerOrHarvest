using System.Collections.Generic;
using GameLogic.View;

namespace GameLogic.Controller
{
	public class SaveGameCmd : CommandBase {
		public SaveGameCmd(List<string> args) : base(args) {}

		public override string CmdTitle => "保存游戏";
		public override string Description => "";
		public override string FailReason => "";
		public override int ArgCount => 0;

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			GameModelMgr.Inst.SaveGame();
			GameViewMgr.Inst.SaveGame();
		}
	}
}