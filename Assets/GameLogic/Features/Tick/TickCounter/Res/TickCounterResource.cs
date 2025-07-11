using System.Collections.Generic;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;

namespace GameLogic.Features.TickCounter {
	/// <summary>
	/// 统计村庄的时间信息
	/// </summary>
	public class TickCounterResource : IResource, ISaveableResource {
		public uint TodayTickCount = 0;
		/// <summary> 从游戏开始运行后到现在的Tick总和 </summary>
		public ulong TickCount = 0;
		public ulong DayCount = 0;
		public bool IsDay;
		public bool IsNight => !IsDay;
		public float DayProcess;
		public float NightProcess;

		public bool IsDayLastTick;
		public bool IsNightLastTick;
		public bool IsDayFirstTick;
		public bool IsNightFirstTick;

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is TickCounterResource tickCounter) {
					TodayTickCount = tickCounter.TodayTickCount;
					TickCount = tickCounter.TickCount;
					DayCount = tickCounter.DayCount;
					IsDay = tickCounter.IsDay;
					DayProcess = tickCounter.DayProcess;
					NightProcess = tickCounter.NightProcess;
					IsDayLastTick = tickCounter.IsDayLastTick;
					IsNightLastTick = tickCounter.IsNightLastTick;
					IsDayFirstTick = tickCounter.IsDayFirstTick;
					IsNightFirstTick = tickCounter.IsNightFirstTick;
					break;
				}
			}
		}
	}
}