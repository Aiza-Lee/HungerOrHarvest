using System.Collections.Generic;
using OldGameLogic.Model.Mgr;
using OldGameLogic.View;
using NSFrame;

namespace OldGameLogic.Controller
{
	public class AutoSaveGameCmd : CommandBase {
		public AutoSaveGameCmd(List<string> args) : base(args) {}

		public override string CmdTitle => "每日结束自动保存游戏";
		public override string Description => "";
		public override string FailReason => "";
		public override int ArgCount => 0;

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			var newSaveInfo = SaveSystem.CreateSaveFile(WorldBaseInfoMgr.Inst.WorldName);
			GameModelMgr.Inst.SetSaveInfo(newSaveInfo);
			GameModelMgr.Inst.SaveGame();
			GameViewMgr.Inst.SetSaveInfo(newSaveInfo);
			GameViewMgr.Inst.SaveGame();
		}
	}
}