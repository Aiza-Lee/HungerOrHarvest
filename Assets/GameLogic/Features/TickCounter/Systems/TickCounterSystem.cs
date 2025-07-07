using NsEcsFrame.Core;

namespace GameLogic.Features.TickCounter {
	/// <summary>
	/// 负责记录Tick
	/// </summary>
	public class SystemName : ISystem {
		public int Priority => -1;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_world.GetResource<TickResource>().TickCount++;
		}
		public void OnRenderUpdate(float _) {}
	} 
}