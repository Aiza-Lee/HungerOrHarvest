using NSFrame;

namespace GameLogic
{
	public class NewWorldCmd : ICommand {
		public string CmdTitle => "新建世界";
		public string Description => "";
		public string FailReason => "";

		public bool Check() {
			return true;
		}

		public void Execute() {
			var saveInfo = SaveSystem.CreateSaveFile();
			SaveMgr.Inst.SaveInfo = saveInfo;
			WorldGenerator.Inst.Generate();
			SaveMgr.Inst.SaveGame();
		}

		public ICommand Init(ICmdData _) {
			return this;
		}
	}

	public class NewWorldCmdData : ICmdData {}
}