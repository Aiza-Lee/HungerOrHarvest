using UnityEngine;

namespace GameLogic.Features.Vill {
	[CreateAssetMenu(fileName = "体力配置", menuName = "HungerOrHarvest/Config/Vill/体力配置", order = 100)]
	public class VitConfig : ScriptableObject {
		[Header("体力阈值配置")]
		[Tooltip("饥饿阈值")] public float HungryVitThreshold = 0.1f; // 默认10%
		[Tooltip("低体力阈值")] public float LowVitThreshold = 0.2f; // 默认20%
		[Tooltip("体力恢复阈值")] public float RecoverVitThreshold = 0.6f; // 默认60%

		[Header("体力影响")]
		[Tooltip("体力低于饥饿阈值时的生产效率损失")] public float HungryProdLoss = 0.5f;

		[Header("体力恢复配置")]
		[Tooltip("每单位食物恢复的体力量")] public float VitPerFood = 0.5f;
		[Tooltip("默认最大体力值")] public float MaxVit = 100f;
		[Tooltip("每Tick消耗食物")] public float FoodConsPerTickWhenRecover = 0.1f;
		[Tooltip("每日恢复体力的次数")] public int RecoverChancePerDay = 1;

		[Header("体力消耗配置")]
		[Tooltip("白天状态下每Tick体力消耗，默认一直消耗")] public float DayVitConsPerTick = 0.01f;
		[Tooltip("Dying状态下的每Tick体力消耗")] public float DyingVitConsPerTick = 0.05f;
	}
}