using NSFrame;

namespace GameLogic
{
	public class LoadSaveCmd : ICommand {
		private SaveInfo _saveInfo;

		public string CmdTitle => "加载存档";
		public string Description => $"存档名称:{_saveInfo.SaveName}";
		public string FailReason => "saveInfo为null";

		public bool Check() {
			return _saveInfo != null;
		}

		public void Execute() {
			SaveMgr.Inst.SaveInfo = _saveInfo;
			SaveMgr.Inst.LoadGame();
		}

		public ICommand Init(ICmdData data) {
			var d = (LoadSaveCmdData)data;
			_saveInfo = d.saveInfo;
			return this;
		}
	}
	public class LoadSaveCmdData : ICmdData {
		public SaveInfo saveInfo;
	}
}