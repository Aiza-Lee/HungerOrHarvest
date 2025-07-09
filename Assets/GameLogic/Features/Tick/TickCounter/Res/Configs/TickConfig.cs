using UnityEngine;

namespace GameLogic.Features.TickCounter {
	[CreateAssetMenu(fileName = "TickConfig", menuName = "HungerOrHarvest/Config/TickConfig", order = 1)]
	public class TickConfig : ScriptableObject {
		[Tooltip("白天Tick数量")] public uint DAY_TICKS;
		[Tooltip("夜晚Tick数量")] public uint NIGHT_TICKS;
		[Tooltip("夜晚时间倍速")] public float NIGHT_TIME_SPEED;
	}
}