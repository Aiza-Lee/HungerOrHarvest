using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Features.Arch;
using GameLogic.Features.Generator;
using GameLogic.Features.Repo;
using NsEcsFrame.Core;

namespace GameLogic.Features.NewWorldCreator {
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
			var baseInfo = infoRes.NewWorldInfo.BaseInfo;
			var worldName = infoRes.NewWorldInfo.WorldName;
			ClearWorld.ClearWorldAPI.Clear();
			_world.Name = worldName;
			CreateWorld(baseInfo);
			infoRes.NewWorldInfo = null; // 清除创建信息，避免重复创建
		}
		public void OnRenderUpdate(float _) { }

		private void CreateWorld(RandomWorldBaseInfo info) {
			var repoRes = _world.GetResource<RepoStatResource>();
			foreach (var unlock in info.UnlockedRepos) {
				repoRes.Unlocked_F[unlock.EnumType] = true;
				repoRes.RepoMax_F[unlock.EnumType] = unlock.Value;
			}
			foreach (var repo in info.InitialRepos) {
				repoRes.Repos_F[repo.EnumType] = repo.Value;
			}

			var archUnlockRes = _world.GetResource<UnlockedArchResource>();
			foreach (var unlock in info.UnlockedArchs) {
				archUnlockRes.Unlocked_F[unlock] = true;
			}

			var mid = info.Layers.Count / 2;
			for (int i = 0; i < info.Layers.Count; i++) {
				var layerType = info.Layers[i];
				// todo: 等到美术资源确定层的长度后，再决定这里如何生成，生成几个layer对象
				LayerGenerateAPI.GenerateLayer(layerType, new(0, i - mid + ConstMgr.MIDDLE_LAYER));
			}
			foreach (var arch in info.Archs) {
				ArchGenerateAPI.GenerateArch(arch.EnumType, arch.Value + ConstMgr.WORLD_CENTER_OL);
			}
			foreach (var vill in info.Vills) {
				VillGenerateAPI.GenerateVill(vill.EnumType, vill.Value + ConstMgr.WORLD_CENTER_OL);
			}
		}
	} 
}