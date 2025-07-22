using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Elements.Decorations;
using GameLogic.Features.Generator;
using GameLogic.Features.MainCamera;
using GameLogic.Features.Repo;
using GameLogic.Features.SpeedControl;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.WorldDataManager {
	/// <summary>
	/// NewWorldCreatorSystem 负责创建新世界的逻辑。
	/// </summary>
	public class NewWorldCreatorSystem : ISystem {
		public int Priority => -100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var infoRes = _world.GetResource<NewWorldInfoResource>();
			if (infoRes.NewWorldInfo == null) return;
			WorldDataManagerAPI.ClearWorld();

			var baseInfo = infoRes.NewWorldInfo.BaseInfo;
			var worldName = infoRes.NewWorldInfo.WorldName;
			_world.Name = worldName;
			CreateWorld(baseInfo);
			infoRes.NewWorldInfo = null; // 清除创建信息，避免重复创建
			_world.GetResource<CurSaveInfoResource>().IsLoaded = true;
		}
		public void OnRenderUpdate(float _) { }

		private void CreateWorld(RandomWorldBaseInfo info) {

			var repoRes = _world.GetResource<RepoStatResource>();
			// 解锁初始仓库和建筑
			foreach (var unlock in info.UnlockedRepos) {
				repoRes.Unlocked_F[unlock.EnumType] = true;
				repoRes.RepoMax_F[unlock.EnumType] = unlock.Value;
			}
			// 初始资源
			foreach (var repo in info.InitialRepos) {
				repoRes.Repos_F[repo.EnumType] = repo.Value;
			}

			// 解锁建筑
			var archUnlockRes = _world.GetResource<UnlockedArchResource>();
			foreach (var unlock in info.UnlockedArchs) {
				archUnlockRes.Unlocked_F[unlock] = true;
			}

			// 生成layer和装饰物
			var mid = info.Layers.Count / 2;
			var rates = info.DecorationRates;
			for (int i = 0; i < info.Layers.Count; i++) {
				var layerType = info.Layers[i];
				var lyr = i - mid + ConstMgr.MIDDLE_LAYER;
				LayerGenerateAPI.Generate(layerType, new(0, lyr));

				// 生成装饰物
				if (layerType != LayerType.Grass) continue;
				for (int cx = 0; cx <= ConstMgr.MAX_CX; ++cx) {
					var coord = new Coord(cx, ConstMgr.CY_PER_LYR * lyr);

					// 随机生成装饰物，采用轮盘抽样
					var p = Random.Range(0f, 1f);
					var pSum = 0f;
					foreach (var pr in rates) {
						pSum += pr.Value;
						if (p < pSum) {
							SpawnDecoration(pr.Key, coord);
							break;
						}
					}

				}
			}
			// 生成建筑
			foreach (var arch in info.Archs) {
				ArchGenerateAPI.Generate(arch.EnumType, arch.Value + ConstMgr.WORLD_CENTER_OL);
			}
			// 生成村民
			foreach (var vill in info.Vills) {
				VillGenerateAPI.Generate(vill.EnumType, (vill.Value + ConstMgr.WORLD_CENTER_OL).ToCoord());
			}


			WorldDataManagerAPI.Save(false);
			SpeedControlAPI.SetSpeedControlInputEnabled(true);
		}

		private bool SpawnDecoration(DecorationType type, Coord coord) {
			// 随机缩放尺寸
			var randomScale = Random.Range(0.8f, 1.2f);
			DecorationGeneratorAPI.Generate(
				type,
				coord,
				new(randomScale, randomScale, randomScale),
				Random.Range(0, 2) == 0 // 随机翻转X轴
			);
			return true;
		}
	}
}