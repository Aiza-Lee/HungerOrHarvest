using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.TickSpeed {
	/// <summary>
	/// 处理游戏Tick速度的系统
	/// </summary>
	public class TickSpeedSystem : ISystem {
		public int Priority => 10100;
		public bool Enabled { get; set; }
		private IWorld _world;
		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
		}
		public void OnRenderUpdate(float _) {
			var speedRes = _world.GetResource<TickSpeedResource>();
			if (speedRes.IsDirty) {
				speedRes.IsDirty = false;
				if (speedRes.IsPaused) {
					Time.fixedDeltaTime = float.MaxValue / 2f;
					Time.timeScale = 0f;
				} else {
					Time.fixedDeltaTime = 1f / ConstMgr.SPEEDx1_TICKS_PER_SECOND / speedRes.TickSpeed;
					Time.timeScale = speedRes.TickSpeed;
				}
				if (_world.EnableDebugLogs) {
					Debug.Log($"TickSpeedSystem: TickSpeed changed to {speedRes.TickSpeed}, IsPaused: {speedRes.IsPaused}");
				}
			}
		}
	} 
}