using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.TickSpeed {
	/// <summary>
	/// 处理游戏Tick速度的系统
	/// </summary>
	public class TickSpeedSystem : ISystem {
		public int Priority => 0;
		public bool Enabled { get; set; }
		private IWorld _world;
		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var speedRes = _world.GetResource<TickSpeedResource>();
			if (speedRes.IsDirty) {
				speedRes.IsDirty = false;
				Time.timeScale = speedRes.IsPaused ? 0f : speedRes.TickSpeed;
				if (speedRes.IsPaused) {
					Time.fixedDeltaTime = 0f;
				} else {
					Time.fixedDeltaTime = 0.02f * speedRes.TickSpeed;
				}
				if (_world.EnableDebugLogs) {
					Debug.Log($"TickSpeedSystem: TickSpeed changed to {speedRes.TickSpeed}, IsPaused: {speedRes.IsPaused}");
				}
			}
		}
		public void OnRenderUpdate(float _) { }
	} 
}