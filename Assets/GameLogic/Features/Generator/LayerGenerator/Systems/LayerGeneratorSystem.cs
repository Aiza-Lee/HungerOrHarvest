using GameLogic.Common.View;
using GameLogic.Features.Layer;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// LayerGeneratorSystem 负责生成层的实体。
	/// </summary>
	public class LayerGeneratorSystem : ISystem {
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
			var gInfos = _world.GetResource<LayerGeneratorResource>().LayerGenerateInfos;
			if (gInfos.Count == 0) return;

			foreach (var gInfo in gInfos) {
				GenerateLayer(gInfo);
			}
			gInfos.Clear();
		}

		private void GenerateLayer(LayerGenerateInfo gInfo) {
			var layer = _world.CreateEntity();
			layer.AddComponent<TransformComponent>()
				 .AddComponent<SpriteRendererComponent>()
				 .AddComponent<SmoothedCoordComponent>(
					new() { Coord = gInfo.Coord, ChangeCurveType = ChangeCurveType.Directive, IsDirty = true }
				 )
				 .AddComponent<SmoothChangeStatComponent>()
				 .AddComponent<LayerIdentityComponent>(gInfo.LayerIdentity)
			;
			var type = gInfo.LayerIdentity.LayerType;
			var ac = _world.GetResource<LayerConfigResource>().GetArtConfig(type);
			var go = GameObject.Instantiate(ac.Prefab);
			go.GetComponent<LayerEntityMono>().SetEntity(layer);
		}

		public void OnRenderUpdate(float _) { }
	} 
}