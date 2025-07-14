using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	/// <summary>
	/// 负责把逻辑事件转化为对应的视觉事件，如果转化成功，会移除原来的逻辑事件
	/// </summary>
	public class LogicFrameRequestConversionSystem : ISystem {
		public int Priority => 1900;
		public bool Enabled { get; set; }
		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }

		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) { }
	}
}