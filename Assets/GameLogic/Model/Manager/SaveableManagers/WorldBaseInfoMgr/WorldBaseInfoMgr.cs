using System;

namespace GameLogic
{
	sealed public class WorldBaseInfoMgr : ISaveable<WorldBaseInfoMgrSave>, IMananger {
		private WorldBaseInfoMgr() {}
		public static WorldBaseInfoMgr Inst { get; } = new();
		
		public string WorldName {get; set;}
		private string _worldHashTag;

		public void SetWorldHashTag() {
			_worldHashTag = Guid.NewGuid().ToString();
		}

		public WorldBaseInfoMgrSave GetSave() {
			return new WorldBaseInfoMgrSave { 
				WorldName = WorldName, 
				WorldHashID = _worldHashTag,
			};
		}

		public void InitFromSave(WorldBaseInfoMgrSave save) {
			WorldName 		= save.WorldName;
			_worldHashTag 	= save.WorldHashID;
		}

		#region IManager
		public void ClearMgr() {}
		#endregion
	}
}