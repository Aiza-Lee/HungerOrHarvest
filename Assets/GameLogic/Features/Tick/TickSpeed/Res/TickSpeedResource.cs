using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.TickSpeed {
	/// <summary>
	/// 统计村庄的时间信息
	/// </summary>
	public class TickSpeedResource : IResource {
		[Tooltip("一倍速对应每秒50逻辑帧")] public float TickSpeed = 1f;
		public bool IsPaused = false;

		public bool IsDirty = false;
	}
}