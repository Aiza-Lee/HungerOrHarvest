using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	/// <summary>
	/// 游戏从始至末的管理
	/// </summary>
	public class GameModelMgr : MonoSingleton<GameModelMgr> {

		private SaveInfo _saveInfo;

		private  List<IMananger> _Mgrs;

		private void Start() {
			_Mgrs = new() {
				MapMgr.Inst,
				DisasterMgr.Inst,
				IDMgr.Inst,
				LogicTimeMgr.Inst,
				RepoMgr.Inst,
				RouteMgr.Inst,
				RoomMgr.Inst,
				WorldMgr.Inst,
			};
			EventSystem.Invoke((int)ModelEvt.MgrInitAfterMonoMgr, NSFrame.EventType.Model);
		}


		#region PublicMethods
		public bool SaveInfoSeted() => _saveInfo != null;
		public void SetSaveInfo(SaveInfo saveInfo) {
			_saveInfo = saveInfo;
		}

		public void SaveGame() {
			SaveSystem.SaveObjects( _saveInfo, 
				WorldMgr.Inst.GetSave(),
				IDMgr.Inst.GetSave(),
				LogicTimeMgr.Inst.GetSave(),
				RepoMgr.Inst.GetSave(),
				DisasterMgr.Inst.GetSave(),
				WorldBaseInfoMgr.Inst.GetSave()
			);
		}

		public void LoadGame() {
			ClearAllMgrs();
			WorldMgr.Inst			.InitFromSave(SaveSystem.LoadObject<WorldSave>(_saveInfo));
			IDMgr.Inst				.InitFromSave(SaveSystem.LoadObject<IDMgrSave>(_saveInfo));
			LogicTimeMgr.Inst		.InitFromSave(SaveSystem.LoadObject<LogicTimeMgrSave>(_saveInfo));
			RepoMgr.Inst			.InitFromSave(SaveSystem.LoadObject<RepoMgrSave>(_saveInfo));
			DisasterMgr.Inst		.InitFromSave(SaveSystem.LoadObject<DisasterMgrSave>(_saveInfo));
			WorldBaseInfoMgr.Inst	.InitFromSave(SaveSystem.LoadObject<WorldBaseInfoMgrSave>(_saveInfo));
		}

		public void ClearAllMgrs() {
			_Mgrs.ForEach(mgr => mgr.ClearMgr());
		}
		#endregion
	}
}