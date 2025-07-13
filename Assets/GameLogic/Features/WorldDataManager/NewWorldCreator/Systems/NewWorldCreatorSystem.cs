using GameLogic.Common.Logic;
using GameLogic.Features.Arch;
using GameLogic.Features.Generator;
using GameLogic.Features.MainCamera;
using GameLogic.Features.Repo;
using GameLogic.Features.SpeedControl;
using GameLogic.World;
using NsEcsFrame.Core;

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
			_world.GetResource<SaveInfoResource>().IsLoaded = true;
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

			// 生成layer
			var mid = info.Layers.Count / 2;
			for (int i = 0; i < info.Layers.Count; i++) {
				var layerType = info.Layers[i];

				float ux = 0f;
				int odr = 0;
				while (ux < ConstMgr.MAX_UX) {
					LayerGenerateAPI.GenerateLayer(layerType, new(odr, i - mid + ConstMgr.MIDDLE_LAYER));
					odr += (int) (ConstMgr.LAYER_SPRITE_UX_LENGTH / (ConstMgr.UX_PER_CX * ConstMgr.CX_PER_ODR));
					ux += ConstMgr.LAYER_SPRITE_UX_LENGTH;
				}
			}
			// 生成建筑
			foreach (var arch in info.Archs) {
				ArchGenerateAPI.GenerateArch(arch.EnumType, arch.Value + ConstMgr.WORLD_CENTER_OL);
			}
			// 生成村民
			foreach (var vill in info.Vills) {
				VillGenerateAPI.GenerateVill(vill.EnumType, (vill.Value + ConstMgr.WORLD_CENTER_OL).ToCoord());
			}

			WorldDataManagerAPI.Save(false);
			SpeedControlAPI.SetSpeedControlInputEnabled(true);
			CameraInputAPI.SetCameraInputEnabled(true);
		}
	} 
}