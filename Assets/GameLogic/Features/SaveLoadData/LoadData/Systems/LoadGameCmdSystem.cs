using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	/// <summary>
	/// LoadGameCmdSystem 负责响应加载游戏的命令。
	/// </summary>
	public class LoadGameCmdSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var loadCmd = _world.GetResource<LoadGameCmdResource>();
			if (!loadCmd.LoadGameCommand) return;
			loadCmd.LoadGameCommand = false;
			var saveInfo = _world.GetResource<SaveInfoResource>().SaveInfo;
			var gameData = SaveSystem.LoadObject<GameSaveData>(saveInfo);

			_world.DestroyAllEntities();
			GidMgr.Inst = gameData.GidMgr;
			gameData.SavedResources.ForEach(res => _world.InsertResource(res));

			var entitiesData = gameData.EntitiesSaveData;
			entitiesData.Entities.ForEach(entityData => {
				var entity = _world.CreateEntity();
				entityData.Components.ForEach(comp => entity.AddComponent(comp));
			});
		}
		public void OnRenderUpdate(float _) { }
	}
}