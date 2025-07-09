using GameLogic.Common.Logic;
using GameLogic.Common.View;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// 负责生成村民
	/// </summary>
	public class VillGeneratorSystem : ISystem {
		public int Priority => 500;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var datas = _world.GetResource<VillGeneratorResource>().VillDatas;
			if (datas.Count == 0) return;
			
			foreach (var data in datas) {
				GenerateVill(data);
			}
			datas.Clear();
		}
		public void OnRenderUpdate(float _) { }

		private void GenerateVill(VillGenerateData data) {
			var configRes = _world.GetResource<VillConfigResource>();
			var type = data.Type;
			var config = configRes.GetConfig(type);
			var vill = config.GetDefaultEntity(_world);

			var coordComp = vill.GetComponent<SmoothedCoordComponent>();
			coordComp.Coord = data.Coord;
			coordComp.IsDirty = true;

			var gidComp = vill.GetComponent<GidComponent>();
			gidComp.Gid = GidMgr.Inst.GetGid();
			GameWorldMono.GidToEntity[gidComp.Gid] = vill;
		var artConfig = configRes.GetArtConfig(type);			
		var go = GameObject.Instantiate(artConfig.Prefab);
		go.GetComponent<VillEntityMono>().SetEntity(vill);

		var eventEntity = _world.CreateEntity();
		eventEntity.AddComponent(new VillGeneratedEventComp() { VillGid = gidComp.Gid });
	}
	}
}