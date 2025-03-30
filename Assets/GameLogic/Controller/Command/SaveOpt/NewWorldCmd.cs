using System.Collections.Generic;
using NSFrame;

namespace GameLogic
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
			var saveInfo = SaveSystem.CreateSaveFile();
			GameMgr.Inst.RegisterSaveInfo(saveInfo);
			WorldGenerator.Inst.Generate();
			GameMgr.Inst.SaveGame();
		}
	}
}