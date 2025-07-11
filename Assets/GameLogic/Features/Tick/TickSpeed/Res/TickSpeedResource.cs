using GameLogic.Features.ClearWorld;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.TickSpeed {
	/// <summary>
	/// 统计村庄的时间信息
	/// </summary>
	public class TickSpeedResource : IResource, IWorldClearRespondable {
		[Tooltip("一倍速对应每秒50Ticks")] public float TickSpeed = 1f;
		public bool IsPaused = true;
		
		public bool IsDirty = true;

		public void RespondWorldClear() {
			IsPaused = true;
			TickSpeed = 1f;
			IsDirty = true;
		}
	}
}