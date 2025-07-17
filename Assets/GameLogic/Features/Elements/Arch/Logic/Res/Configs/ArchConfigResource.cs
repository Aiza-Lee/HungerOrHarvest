using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Arch {
	[System.Serializable]
	public class ArchConfigResource : IResource {
		[SerializeReference][SerializeField][Tooltip("没有找到配置时的默认配置")] private ArchConfigBase DefaultConfig;
		[SerializeReference][SerializeField][Tooltip("没有找到配置时的默认配置")] private ArchArtConfigBase DefaultArtConfig;
		[SerializeReference][SerializeField] private List<ArchConfigBase> Configs = new();
		[SerializeReference][SerializeField] private List<ArchArtConfigBase> ArtConfigs = new();

		private Dictionary<ArchType, ArchConfigBase> _configs;
		private Dictionary<ArchType, ArchArtConfigBase> _artConfigs;
		public ArchConfigBase GetConfig(ArchType archType) {
			if (_configs == null) {
				_configs = new Dictionary<ArchType, ArchConfigBase>();
				foreach (var c in Configs) {
					_configs[c.ArchType] = c;
				}
			}
			return _configs.TryGetValue(archType, out var config) ? config : DefaultConfig;
		}
		public ArchArtConfigBase GetArtConfig(ArchType archType) {
			if (_artConfigs == null) {
				_artConfigs = new Dictionary<ArchType, ArchArtConfigBase>();
				foreach (var c in ArtConfigs) {
					_artConfigs[c.ArchType] = c;
				}
			}
			return _artConfigs.TryGetValue(archType, out var artConfig) ? artConfig : DefaultArtConfig;
		}
	}

	public abstract class ArchConfigBase : ScriptableObject {
		abstract public ArchType ArchType { get; }

		public void TryAddComponentsToEntity(Entity entity) {
			var prefab = ArchQueryAPI.GetArtConfig(ArchType).Prefab;
			entity
				.TryAddComponent<GidComponent>()
				.TryAddComponent<ArchIdentityComponent>(new() { ArchType = ArchType })
				.TryAddComponent<ArchLevelComponent>()
				.TryAddComponent<TransformComponent>(new(prefab.GetComponent<Transform>()))
				.TryAddComponent<SpriteRendererComponent>(new(prefab.GetComponent<SpriteRenderer>()))
				.TryAddComponent<OLComponent>()
				.TryAddComponent<SavedEntityComponent>()
				.TryAddComponent<BondToVillComponent>()
				.TryAddComponent<VillContainerComponent>()
			;
			TryAddDerivedComponents(entity);
		}
		protected abstract void TryAddDerivedComponents(Entity entity);

		[Tooltip("建筑名称")] public string Name;
		[Tooltip("建造所需时间（Tick）")] public int ConstructTicks;
		[Tooltip("建造消耗（资源类型-数量）")] public ReadOnlyEtList<RepoType, float> ConstructCost;
		[Tooltip("拆除返还比例")] public float DeconstructRate;
		[Tooltip("修复需要的资源占建造资源的比重")] public float RepairRate;
		[Tooltip("各等级配置")] public List<ArchLevelConfigBase> LevelConfigs;
	}

	public abstract class ArchLevelConfigBase : ScriptableObject {
		[Tooltip("等级")] public int Level;
		[Tooltip("容纳人数上限")] public int MaxVillContain;
		[Tooltip("介绍")][TextArea(5, 30)] public string Introduction;
		[Tooltip("固有产出")] public ReadOnlyEtList<RepoType, float> SelfProdPerTick;
		[Tooltip("额外产出/每人")] public ReadOnlyEtList<RepoType, float> ExtraProdPerOnePerTick;
		[Tooltip("固有消耗")] public ReadOnlyEtList<RepoType, float> SelfConsPerTick;
		[Tooltip("额外消耗/每人")] public ReadOnlyEtList<RepoType, float> ExtraConsPerOnePerTick;
		[Tooltip("存储量增量")] public ReadOnlyEtList<RepoType, float> AddRepoVolume;
		[Tooltip("职业经验的增量")] public ReadOnlyEtList<JobType, float> AddExpPerTick;
		[Tooltip("体力消耗速率")] public float VitConsPerTick;
	}

	public abstract class ArchArtConfigBase : ScriptableObject {
		public abstract ArchType ArchType { get; }
		[Tooltip("World中展示的精灵")] public Sprite WorldSprite;
		[Tooltip("Map中在地图上展示的精灵")] public Sprite MapSprite;
		[Tooltip("建筑动画控制器")] public Animator Animator;
		[Tooltip("预制体对象")] public GameObject Prefab;
	}

}