using GameLogic.Common.Logic;
using GameLogic.Features.Layer;
using GameLogic.Features.WorldDataManager;
using GameLogic.World;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// LayerGeneratorSystem 负责生成层的实体。
	/// </summary>
	public class LayerGeneratorSystem : ISystem {
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
			var datas = _world.GetResource<LayerGeneratorResource>().LayerDatas;
			if (datas.Count == 0) return;

			foreach (var data in datas) {
				GenerateLayer(data);
			}
			datas.Clear();
		}

		private void GenerateLayer(LayerGenerateData data) {
			var type = data.Type;
			var config = _world.GetResource<LayerConfigResource>().GetConfig(type);

			bool newGid = true;
			var entity = _world.CreateEntity();
			foreach (var comp in data.ExtraComponents) {
				if (comp is GidComponent) newGid = false;
				entity.AddComponent(comp);
			}
			config.TryAddComponentsToEntity(entity);

			var olComp = entity.GetComponent<OLComponent>();
			olComp.OL = data.OL;
			olComp.IsDirty = true;

			var gidComp = entity.GetComponent<GidComponent>();
			if (newGid) {
				gidComp.Gid = GidMgr.Inst.GetGid();
			}
			GameWorldMono.GidToEntity[gidComp.Gid] = entity;

			var ac = _world.GetResource<LayerConfigResource>().GetArtConfig(type);
			var go = GameObject.Instantiate(ac.Prefab);
			go.GetComponent<LayerEntityMono>().SetEntity(entity);

			_world.CreateEntity()
				.AddComponent(new LayerGeneratedEventComp_Logic() { LayerGid = gidComp.Gid })
				.AddComponent<SavedEntityComponent>();
		}

		public void OnRenderUpdate(float _) { }
	}
}