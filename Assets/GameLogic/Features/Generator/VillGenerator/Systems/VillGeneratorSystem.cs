using GameLogic.Common.View;
using GameLogic.Features.Vill;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 负责生成村民
	/// </summary>
	public class VillGeneratorSystem : ISystem {
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
			var gInfos = _world.GetResource<VillGeneratorResource>().VillGenerateInfos;
			if (gInfos.Count == 0) return;
			foreach (var gInfo in gInfos) {
				GenerateVill(gInfo);
			}
		}
		public void OnRenderUpdate(float _) { }

		private void GenerateVill(VillGenerateInfo gInfo) {
			var vill = _world.CreateEntity();
			vill.AddComponent<SmoothedCoordComponent>(
					new() { Coord = gInfo.Coord, ChangeCurveType = ChangeCurveType.Linear, IsDirty = true }
				)
				.AddComponent<SmoothChangeStatComponent>()
				.AddComponent<TransformComponent>()
				.AddComponent<SpriteRendererComponent>()
				.AddComponent<AddJobExpComponent>()
				.AddComponent<BondToArchComponent>()
				.AddComponent<JobExpComponent>(gInfo.VillJobExp)
				.AddComponent<RoutePlanComponent>()
				.AddComponent<VillBehaviourTreeComponent>()
				.AddComponent<VillIdentityComponent>(gInfo.VillIdentity)
				.AddComponent<VillMoveComponent>()
				.AddComponent<VillVitalityComponent>(gInfo.VillVitalityState);
			var type = gInfo.VillIdentity.Type;
			var ac = _world.GetResource<VillConfigResource>().GetArtConfig(type);
			var go = GameObject.Instantiate(ac.Prefab);
			go.GetComponent<VillEntityMono>().SetEntity(vill);
		}
	}
}