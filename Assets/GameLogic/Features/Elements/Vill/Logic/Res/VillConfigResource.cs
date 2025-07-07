using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[System.Serializable]
	public class VillConfigResource : IResource {
		[SerializeReference] public List<VillConfigBase> Configs = new();
	}

	public abstract class VillConfigBase : ScriptableObject {
		abstract public VillType VillType { get; }
		[Header("体力配置")] public VitConfig VitConfig;
		[Header("随机游走横向半径(相对于已解锁的地块,计量单位是ORD)")] public int SpareOrdRadius;
		[Header("动画")] public Animator Animator;
	}

	[CreateAssetMenu(fileName = "VitalityConfig", menuName = "HungerOrHarvest/Config/Vill/VitConfig")]
	public class VitConfig : ScriptableObject {
		[Header("体力阈值配置")]
		[Tooltip("饥饿阈值，当体力百分比低于此值时村民会进入饥饿状态")] public float HungryVitThreshold = 0.1f; // 默认10%
		[Tooltip("低体力阈值，当体力百分比低于此值时村民会去吃饭")] public float LowVitThreshold = 0.2f; // 默认20%
		[Tooltip("体力恢复阈值，当体力百分比高于此值时村民会返回工作")] public float RecoverVitThreshold = 0.6f; // 默认60%

		[Header("体力对效率的影响")]
		[Tooltip("体力低于饥饿阈值时的生产效率损失")] public float HungryProdLoss = 0.5f;

		/// <summary>
		/// 每单位食物恢复的体力量
		/// </summary>
		[Header("体力恢复配置")]
		[Tooltip("每单位食物恢复的体力量")] public float VitPerFood = 0.5f;

		/// <summary>
		/// 默认最大体力值
		/// </summary>
		[Tooltip("默认最大体力值")] public float MaxVit = 100f;

		/// <summary>
		/// 消耗食物速率
		/// </summary>
		[Tooltip("消耗食物速率")] public float TickFoodCons = 0.1f;

		[Tooltip("每日恢复体力的次数")] public int RecoverChancePerDay = 1;

		[Header("体力消耗配置")]
		[Tooltip("白天状态下的体力消耗速率，默认一直消耗")] public float TickDayVitCons = 0.01f;
		[Tooltip("Dying状态下的体力消耗速率")] public float TickDyingVitCons = 0.05f;
	}
}