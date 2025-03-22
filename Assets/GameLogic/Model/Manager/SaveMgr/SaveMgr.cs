using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public sealed class SaveMgr {
		private SaveMgr() {}
		public static SaveMgr Inst { get; } = new();

		private SaveInfo _saveInfo;
		public SaveInfo SaveInfo { get => _saveInfo; set => _saveInfo = value; }

		public List<SaveInfo> GetSaveInfos() {
			return SaveSystem.GetAllSaveInfos();
		}

		public void SaveGame() {
			SaveSystem.SaveObjects( _saveInfo, 
				WorldMgr.Inst.GetSave(),
				IDMgr.Inst.GetSave(),
				LogicTimeMgr.Inst.GetSave(),
				RepoMgr.Inst.GetSave(),
				DisasterMgr.Inst.GetSave()
			);
		}

		public void LoadGame() {
			WorldMgr.Inst.InitFromSave(SaveSystem.LoadObject<WorldSave>(_saveInfo));
			IDMgr.Inst.InitFromSave(SaveSystem.LoadObject<IDMgrSave>(_saveInfo));
			LogicTimeMgr.Inst.InitFromSave(SaveSystem.LoadObject<LogicTimeMgrSave>(_saveInfo));
			RepoMgr.Inst.InitFromSave(SaveSystem.LoadObject<RepoMgrSave>(_saveInfo));
			DisasterMgr.Inst.InitFromSave(SaveSystem.LoadObject<DisasterMgrSave>(_saveInfo));
		}

	}
}