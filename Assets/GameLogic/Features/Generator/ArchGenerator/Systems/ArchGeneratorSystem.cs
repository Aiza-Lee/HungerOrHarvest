using GameLogic.Common.Logic;
using GameLogic.Features.Arch;
using GameLogic.World;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// ArchGeneratorSystem 负责生成建筑。
	/// </summary>
	public class ArchGeneratorSystem : ISystem {
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
			var datas = _world.GetResource<ArchGeneratorResource>().ArchDatas;
			if (datas.Count == 0) return;

			foreach (var data in datas) {
				GenerateArch(data);
			}
			datas.Clear();
		}
		public void OnRenderUpdate(float _) { }

		private void GenerateArch(ArchGenerateData data) {
			var type = data.Type;
			var config = _world.GetResource<ArchConfigResource>().GetConfig(type);
			var entity = config.GetDefaultEntity(_world);

			var olComp = entity.GetComponent<OLComponent>();
			olComp.OL = data.OL;
			olComp.IsDirty = true;

			var gidComp = entity.GetComponent<GidComponent>();
			gidComp.Gid = GidMgr.Inst.GetGid();
			GameWorldMono.GidToEntity[gidComp.Gid] = entity;

			var ac = _world.GetResource<ArchConfigResource>().GetArtConfig(type);
			var go = GameObject.Instantiate(ac.Prefab);
			go.GetComponent<ArchEntityMono>().SetEntity(entity);

			var eventEntity = _world.CreateEntity();
			eventEntity.AddComponent(new ArchGeneratedEventComp_Logic() { ArchGid = gidComp.Gid });
		}

	} 
}