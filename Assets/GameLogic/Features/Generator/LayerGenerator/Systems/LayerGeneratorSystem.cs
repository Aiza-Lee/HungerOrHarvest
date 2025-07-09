using GameLogic.Common.Logic;
using GameLogic.Features.Layer;
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
			var layer = config.GetDefaultEntity(_world);

			var olComp = layer.GetComponent<OLComponent>();
			olComp.OL = data.OL;
			olComp.IsDirty = true;

			var gidComp = layer.GetComponent<GidComponent>();
			gidComp.Gid = GidMgr.Inst.GetGid();
			GameWorldMono.GidToEntity[gidComp.Gid] = layer;
		var ac = _world.GetResource<LayerConfigResource>().GetArtConfig(type);
		var go = GameObject.Instantiate(ac.Prefab);
		go.GetComponent<LayerEntityMono>().SetEntity(layer);

		var eventEntity = _world.CreateEntity();
		eventEntity.AddComponent(new LayerGeneratedEventComp() { LayerGid = gidComp.Gid });
	}

		public void OnRenderUpdate(float _) { }
	}
}