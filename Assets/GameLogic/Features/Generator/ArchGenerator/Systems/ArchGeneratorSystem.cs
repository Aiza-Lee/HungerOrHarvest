using GameLogic.Common.View;
using GameLogic.Features.Arch;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// ArchGeneratorSystem 负责生成建筑。
	/// </summary>
	public class ArchGeneratorSystem : ISystem {
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
			var gInfos = _world.GetResource<ArchGeneratorResource>().ArchGenerateInfos;
			if (gInfos.Count == 0) return;

			foreach (var gInfo in gInfos) {
				GenerateArch(gInfo);
			}
			gInfos.Clear();
		}
		public void OnRenderUpdate(float _) { }

		private void GenerateArch(ArchGenerateInfo gInfo) {
			var arch = _world.CreateEntity();
			arch.AddComponent<TransformComponent>()
				.AddComponent<SpriteRendererComponent>()
				.AddComponent<SmoothedCoordComponent>(
					new() { Coord = gInfo.Coord, ChangeCurveType = ChangeCurveType.Directive, IsDirty = true }
				)
				.AddComponent<SmoothChangeStatComponent>()
				.AddComponent<ArchIdentityComponent>(gInfo.ArchIdentity);
			var type = gInfo.ArchIdentity.ArchType;
			var ac = _world.GetResource<ArchConfigResource>().GetArtConfig(type);
			var go = GameObject.Instantiate(ac.Prefab);
			go.GetComponent<ArchEntityMono>().SetEntity(arch);
		}
	} 
}