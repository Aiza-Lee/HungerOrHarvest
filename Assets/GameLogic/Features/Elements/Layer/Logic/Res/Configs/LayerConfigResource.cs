using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Features.WorldDataManager;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Layer {
	[System.Serializable]
	public class LayerConfigResource : NsEcsFrame.Core.IResource {
		[SerializeReference][SerializeField] private LayerConfigBase DefaultConfig;
		[SerializeReference][SerializeField] private LayerArtConfigBase DefaultArtConfig;
		[SerializeReference][SerializeField] private List<LayerConfigBase> Configs;
		[SerializeReference][SerializeField] private List<LayerArtConfigBase> ArtConfigs;
		private Dictionary<LayerType, LayerConfigBase> _configs;
		private Dictionary<LayerType, LayerArtConfigBase> _artConfigs;
		public LayerConfigBase GetConfig(LayerType layerType) {
			if (_configs == null) {
				_configs = new Dictionary<LayerType, LayerConfigBase>();
				foreach (var c in Configs) {
					_configs[c.LayerType] = c;
				}
			}
			return _configs.TryGetValue(layerType, out var config) ? config : DefaultConfig;
		}
		public LayerArtConfigBase GetArtConfig(LayerType layerType) {
			if (_artConfigs == null) {
				_artConfigs = new Dictionary<LayerType, LayerArtConfigBase>();
				foreach (var c in ArtConfigs) {
					_artConfigs[c.LayerType] = c;
				}
			}
			return _artConfigs.TryGetValue(layerType, out var artConfig) ? artConfig : DefaultArtConfig;
		}
	}

	public abstract class LayerConfigBase : ScriptableObject {
		public abstract LayerType LayerType { get; }

		public void TryAddComponentsToEntity(Entity entity) {
			entity
				.TryAddComponent<GidComponent>()
				.TryAddComponent<LayerIdentityComponent>(new() { LayerType = LayerType })
				.TryAddComponent<TransformComponent>()
				.TryAddComponent<SpriteRendererComponent>(new() {
					DrawMode = SpriteDrawMode.Tiled,
					TileMode = SpriteTileMode.Continuous,
					Size = new(ConstMgr.MAX_UX, 16),
					Color = GameWorldMono.MainWorld.GetResource<LayerConfigResource>().GetArtConfig(LayerType).Prefab.GetComponent<SpriteRenderer>().color
				})
				.TryAddComponent<OLComponent>()
				.TryAddComponent<SavedEntityComponent>();
			TryAddDerivedComponents(entity);
		}
		protected abstract void TryAddDerivedComponents(Entity entity);

		public string LayerName;
		public string LayerDescription;
	}

	public abstract class LayerArtConfigBase : ScriptableObject {
		public abstract LayerType LayerType { get; }
		[Tooltip("Layer精灵")] public Sprite Sprite;
		[Tooltip("预制体对象")] public GameObject Prefab;
	}
}