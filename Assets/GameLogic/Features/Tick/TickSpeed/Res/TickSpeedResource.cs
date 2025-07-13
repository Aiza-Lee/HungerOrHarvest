using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.TickSpeed {
	/// <summary>
	/// 统计村庄的时间信息
	/// </summary>
	public class TickSpeedResource : IResource, IWorldClearRespondable {
		[Tooltip("一倍速对应每秒20Ticks")] public float TickSpeed = 1f;
		public bool IsPaused = false;
		
		public bool IsDirty = true;

		public void RespondWorldClear() {
			IsPaused = false;
			TickSpeed = 1f;
			IsDirty = true;
		}
	}
}