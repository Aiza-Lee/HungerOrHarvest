using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.SpeedControl {
	/// <summary>
	/// SpeedControlInputSystem 负责处理世界运行速度的输入控制。
	/// </summary>
	public class SpeedControlInputSystem : ISystem {
		public int Priority => 10000;
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

			inputRes.Speed01KeyDown = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
			inputRes.Speed02KeyDown = Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
			inputRes.Speed03KeyDown = Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);
			inputRes.Speed04KeyDown = Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4);

			inputRes.PauseKeyDown = Input.GetKeyDown(KeyCode.Space);
		}
	} 
}