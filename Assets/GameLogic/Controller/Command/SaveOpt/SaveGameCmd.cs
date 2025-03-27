namespace GameLogic
{
	public class SaveGameCmd : CommandBase {
		public SaveGameCmd(string[] args) : base(args) {}

		public override string CmdTitle => "保存游戏";
		public override string Description => "";
		public override string FailReason => "";
		public override int ArgCount => 0;

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			SaveMgr.Inst.SaveGame();
		}
	}
}