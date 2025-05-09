using System.Collections.Generic;
using NSFrame;

namespace GameLogic.View
{
	public class GameViewMgr : MonoSingleton<GameViewMgr>  {
		private SaveInfo _saveInfo;

		private readonly List<IMananger> _clearableMgrs = new();

		public bool SaveInfoSeted() => _saveInfo != null;
		public void SetSaveInfo(SaveInfo saveInfo) {
			_saveInfo = saveInfo;
		}
		public void RegisterClearableMgr(IMananger mgr) {
			_clearableMgrs.Add(mgr);
		}
		public bool UnregisterClearableMgr(IMananger mgr) {
			return _clearableMgrs.Remove(mgr);
		}

		public void SaveGame() {
			SaveSystem.SaveObjects( _saveInfo,
				TechTreeViewMgr.Inst.GetSave()
			);
		}

		public void LoadGame() {
			ClearAllMgrs();
			TechTreeViewMgr.Inst.InitFromSave(SaveSystem.LoadObject<TechTreeMgrViewSave>(_saveInfo));
		}

		public void ClearAllMgrs() {
			_clearableMgrs.ForEach(mgr => mgr.ClearMgr());
		}
	}
}