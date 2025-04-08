using System.Collections.Generic;
using GameLogic.View;
using NSFrame;

namespace GameLogic.Controller
{
	public class NewWorldCmd : CommandBase {
		public NewWorldCmd(List<string> args) : base(args) {}

		public override string CmdTitle => "新建世界";
		public override string Description => "";
		public override string FailReason => "";
		public override int ArgCount => 0;

		public override bool Check() {
			return true;
		}

		public override void Execute() {

			GameViewMgr.Inst.ClearAllMgrs();
			GameModelMgr.Inst.ClearAllMgrs();

			var saveInfo = SaveSystem.CreateSaveFile();
			GameModelMgr.Inst.RegisterSaveInfo(saveInfo);
			GameViewMgr.Inst.RegisterSaveInfo(saveInfo);

			WorldGenerator.Inst.Generate();

			GameModelMgr.Inst.SaveGame();
			GameViewMgr.Inst.SaveGame();
		}
	}
}