using System.Linq;
using GameLogic.Common.Logic;
using GameLogic.Features.Arch;
using GameLogic.Features.Generator;
using GameLogic.Features.Layer;
using GameLogic.Features.SpeedControl;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.WorldDataManager {
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

			WorldDataManagerAPI.ClearWorld();

			_world.Name = saveInfo.SaveName;

			GidMgr.Inst = gameData.GidMgr;
			var reses = _world.GetAllResources();
			foreach (var res in reses) {
				if (res is ISaveableResource saveableRes) {
					saveableRes.Load(gameData.SavedResources);
				}
			}

			var entityDatas = gameData.EntitiesSaveData.Entities;
			entityDatas.ForEach(entityData => {

				if (CheckForElement(entityData)) return;

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
				vill.AddComponent(new VillBehaviourTreeComponent(vill));
			});

			saveInfoRes.IsLoaded = true;
			SpeedControlAPI.SetSpeedControlInputEnabled(true);
		}
		public void OnRenderUpdate(float _) { }

		/// <summary>
		/// 检查是否为元素类型的实体，并进行相应的生成操作(通过Generator生成)。
		/// </summary>
		private bool CheckForElement(EntitySaveData saveData) {
			var vill = saveData.Components.OfType<VillIdentityComponent>().FirstOrDefault();
			if (vill != null) {
				var coord = saveData.Components.OfType<CoordComponent>().First();
				if (coord != null) {
					VillGenerateAPI.GenerateVill(vill.Type, coord.Coord, saveData.Components);
					return true;
				}
			}

			var layer = saveData.Components.OfType<LayerIdentityComponent>().FirstOrDefault();
			if (layer != null) {
				var ol = saveData.Components.OfType<OLComponent>().First();
				if (ol != null) {
					LayerGenerateAPI.GenerateLayer(layer.LayerType, ol.OL, saveData.Components);
					return true;
				}
			}

			var arch = saveData.Components.OfType<ArchIdentityComponent>().FirstOrDefault();
			if (arch != null) {
				var ol = saveData.Components.OfType<OLComponent>().First();
				if (ol != null) {
					ArchGenerateAPI.GenerateArch(arch.ArchType, ol.OL, saveData.Components);
					return true;
				}
			}

			return false;
		}
	}
}