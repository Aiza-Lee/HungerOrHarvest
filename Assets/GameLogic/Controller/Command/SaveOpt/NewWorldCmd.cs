using NSFrame;

namespace GameLogic
{
	public class NewWorldCmd : CommandBase {
		public NewWorldCmd(string[] args) : base(args) {}

		public override string CmdTitle => "新建世界";
		public override string Description => "";
		public override string FailReason => "";
		public override int ArgCount => 0;

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			var saveInfo = SaveSystem.CreateSaveFile();
			SaveMgr.Inst.SaveInfo = saveInfo;
			WorldGenerator.Inst.Generate();
			SaveMgr.Inst.SaveGame();
		}
	}
}