using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Vill {
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
		[Tooltip("体力配置")] public VitConfig VitConfig;
		[Tooltip("随机游走横向半径(相对于已解锁的地块,计量单位是ORD)")] public int SpareOrdRadius;
	}

	[CreateAssetMenu(fileName = "VitalityConfig", menuName = "HungerOrHarvest/Config/Vill/VitConfig")]
	public class VitConfig : ScriptableObject {
		[Header("体力阈值配置")]
		[Tooltip("饥饿阈值")] public float HungryVitThreshold = 0.1f; // 默认10%
		[Tooltip("低体力阈值")] public float LowVitThreshold = 0.2f; // 默认20%
		[Tooltip("体力恢复阈值")] public float RecoverVitThreshold = 0.6f; // 默认60%

		[Header("体力对效率的影响")]
		[Tooltip("体力低于饥饿阈值时的生产效率损失")] public float HungryProdLoss = 0.5f;

		[Header("体力恢复配置")]
		[Tooltip("每单位食物恢复的体力量")] public float VitPerFood = 0.5f;
		[Tooltip("默认最大体力值")] public float MaxVit = 100f;
		[Tooltip("每Tick消耗食物")] public float TickFoodCons = 0.1f;
		[Tooltip("每日恢复体力的次数")] public int RecoverChancePerDay = 1;

		[Header("体力消耗配置")]
		[Tooltip("白天状态下每Tick体力消耗，默认一直消耗")] public float TickDayVitCons = 0.01f;
		[Tooltip("Dying状态下的每Tick体力消耗")] public float TickDyingVitCons = 0.05f;
	}

	public abstract class VillArtConfigBase : ScriptableObject {
		abstract public VillType VillType { get; }
		[Tooltip("World中展示的精灵")] public Sprite WorldSprite;
		[Tooltip("Map中在地图上展示的精灵")] public Sprite MapSprite;
		[Tooltip("建筑动画控制器")] public Animator Animator;
	}
}