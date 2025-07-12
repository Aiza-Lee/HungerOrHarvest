using GameLogic.Common.Logic;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	/// <summary>
	/// LoadGameCmdSystem 负责响应加载游戏的命令。
	/// </summary>
	public class LoadGameCmdSystem : ISystem {
		public int Priority => 350;
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

			var saveInfoRes = _world.GetResource<SaveInfoResource>();
			var saveInfo = saveInfoRes.SaveInfo;
			var gameData = saveInfo.LoadObject<GameSaveData>();

			SaveLoadDataAPI.ClearWorld();

			GidMgr.Inst = gameData.GidMgr;
			var reses = _world.GetAllResources();
			foreach (var res in reses) {
				if (res is ISaveableResource saveableRes) {
					saveableRes.Load(gameData.SavedResources);
				}
			}

			var entityDatas = gameData.EntitiesSaveData.Entities;
			entityDatas.ForEach(entityData => {
				var entity = _world.CreateEntity();
				entityData.Components.ForEach(comp => {
					entity.AddComponent(comp);
					if (comp is GidComponent gidComp) {
						GameWorldMono.GidToEntity[gidComp.Gid] = entity;
					}
				});
			});

			var vills = _world.CreateQueryBuilder().WithAll<VillIdentityComponent>().Build();
			vills.ForEach(vill => {
				var villAi = vill.GetComponent<VillBehaviourTreeComponent>();
				villAi = new VillBehaviourTreeComponent(vill);
			});

			saveInfoRes.LoadedSave = true;
		}
		public void OnRenderUpdate(float _) { }
	}
}