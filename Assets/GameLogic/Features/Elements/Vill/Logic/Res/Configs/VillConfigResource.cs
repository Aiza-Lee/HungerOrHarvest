using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using GameLogic.Features.WorldDataManager;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	[System.Serializable]
	public class VillConfigResource : IResource {
		[SerializeReference][SerializeField] private VillConfigBase DefaultConfig;
		[SerializeReference][SerializeField] private VillArtConfigBase DefaultArtConfig;
		[SerializeReference][SerializeField] private List<VillConfigBase> Configs = new();
		[SerializeReference][SerializeField] private List<VillArtConfigBase> ArtConfigs = new();
		private Dictionary<VillType, VillConfigBase> _configs;
		private Dictionary<VillType, VillArtConfigBase> _artConfigs;
		public VillConfigBase GetConfig(VillType villType) {
			if (_configs == null) {
				_configs = new Dictionary<VillType, VillConfigBase>();
				foreach (var c in Configs) {
					_configs[c.VillType] = c;
				}
			}
			return _configs.TryGetValue(villType, out var config) ? config : DefaultConfig;
		}
		public VillArtConfigBase GetArtConfig(VillType villType) {
			if (_artConfigs == null) {
				_artConfigs = new Dictionary<VillType, VillArtConfigBase>();
				foreach (var c in ArtConfigs) {
					_artConfigs[c.VillType] = c;
				}
			}
			return _artConfigs.TryGetValue(villType, out var artConfig) ? artConfig : DefaultArtConfig;
		}
	}

	public abstract class VillConfigBase : ScriptableObject {
		abstract public VillType VillType { get; }
		public void TryAddComponentsToEntity(Entity entity) {
			var artConfig = GameWorldMono.MainWorld.GetResource<VillConfigResource>().GetArtConfig(VillType);
			var prefab = artConfig.Prefab;
			var scale = Random.Range(0.85f, 1.05f);
			entity
				.TryAddComponent<GidComponent>()
				.TryAddComponent<VillIdentityComponent>(new() { Type = VillType })
				.TryAddComponent<CoordComponent>()
				.TryAddComponent<SmoothPositionStatComponent>(new(0, ChangeCurveType.Directive, true))
				.TryAddComponent<TransformComponent>(new(prefab.GetComponent<Transform>()) {
					LocalScale = new Vector3(scale, scale, scale)
				})
				.TryAddComponent<SpriteRendererComponent>(new(prefab.GetComponent<SpriteRenderer>()))
				.TryAddComponent<VillBehaviourTreeComponent>(new(entity))
				.TryAddComponent<VillMoveComponent>()
				.TryAddComponent<BondToArchComponent>()
				.TryAddComponent<InArchComponent>()
				.TryAddComponent<JobExpComponent>()
				.TryAddComponent<RoutePlanComponent>()
				.TryAddComponent<VillVitalityComponent>(new() { Vit = VitConfig.MaxVit, RecoverChances = VitConfig.RecoverChancePerDay })
				.TryAddComponent<SavedEntityComponent>()
				.TryAddComponent<VillConfigComponent>(new() {
					LogicConfig = this,
					ArtConfig = artConfig
				})
			;
			AddDerivedComponents(entity);
		}
		protected abstract void AddDerivedComponents(Entity entity);

		[Tooltip("体力配置")] public VitConfig VitConfig;
		// [Tooltip("随机游走横向半径(相对于已解锁的地块,计量单位是ORD)")] public uint SpareOrdRadius;
		[Tooltip("走过每个Coord所需要的Tick数量")] public uint TicksPerCoord;

		public ChangeInfo NormalWalkChangeInfo => new(1f * TicksPerCoord / ConstMgr.SPEEDx1_TICKS_PER_SECOND, ChangeCurveType.Linear, true);
		// public ChangeInfo FastWalkChangeInfo => new(0.5f * TicksPerCoord / ConstMgr.SPEEDx1_TICKS_PER_SECOND, ChangeCurveType.Linear, true);
		// public ChangeInfo SlowWalkChangeInfo => new(2f * TicksPerCoord / ConstMgr.SPEEDx1_TICKS_PER_SECOND, ChangeCurveType.Linear, true);
	}

	public abstract class VillArtConfigBase : ScriptableObject {
		abstract public VillType VillType { get; }
		[Tooltip("World中展示的精灵")] public Sprite WorldSprite;
		[Tooltip("Map中在地图上展示的精灵")] public Sprite MapSprite;
		[Tooltip("村民动画控制器")] public Animator Animator;
		[Tooltip("村民预制体")] public GameObject Prefab;
	}
}