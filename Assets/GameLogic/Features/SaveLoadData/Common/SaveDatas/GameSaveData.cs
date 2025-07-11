using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Features.TickCounter;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.SaveLoadData {

	/// <summary>
	/// 这个类就是游戏逻辑到存档的数据传输中间层
	/// </summary>
	public class GameSaveData {
		public GidMgr GidMgr;
		public List<ISaveableResource> SavedResources = new();
		public EntitiesSaveData EntitiesSaveData;

		public GameSaveData(IWorld world) {
			GidMgr = GidMgr.Inst;
			var reses = GameWorldMono.MainWorld.GetAllResources();
			foreach (var res in reses) {
				if (res is ISaveableResource saveableRes) {
					SavedResources.Add(saveableRes);
				}
			}
			EntitiesSaveData = new(world);
		}
	}
}