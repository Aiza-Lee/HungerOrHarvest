using GameLogic.Features.TickSpeed;
using NsEcsFrame.Core;

namespace GameLogic.Features.SpeedControl {
	/// <summary>
	/// SpeedControlSystem 负责消耗世界运行速度的输入信息
	/// </summary>
	public class SpeedControlSystem : ISystem {
		public int Priority => 10050;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var inputRes = _world.GetResource<SpeedControlInputResource>();
			if (!inputRes.EnabledInput) {
				return;
			}
			var speedRes = _world.GetResource<SpeedControlConfigResource>();
			if (inputRes.Speed01KeyDown) {
				TickSpeedAPI.SetTickSpeed(speedRes.Key1Speed);
			} else if (inputRes.Speed02KeyDown) {
				TickSpeedAPI.SetTickSpeed(speedRes.Key2Speed);
			} else if (inputRes.Speed03KeyDown) {
				TickSpeedAPI.SetTickSpeed(speedRes.Key3Speed);
			} else if (inputRes.Speed04KeyDown) {
				TickSpeedAPI.SetTickSpeed(speedRes.Key4Speed);
			}

			if (inputRes.PauseKeyDown) {
				TickSpeedAPI.TogglePause();
			}
			
		}
	} 
}