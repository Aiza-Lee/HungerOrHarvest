using System.Collections.Generic;
using NSFrame;

namespace GameLogic.View
{
	public class GameViewMgr : MonoSingleton<GameViewMgr>  {
		private SaveInfo _saveInfo;

		private readonly List<IClearMgr> _clearableMgrs = new();

		public void RegisterSaveInfo(SaveInfo saveInfo) {
			_saveInfo = saveInfo;
		}
		public void RegisterClearableMgr(IClearMgr mgr) {
			_clearableMgrs.Add(mgr);
		}
		public bool UnregisterClearableMgr(IClearMgr mgr) {
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