using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// 负责将村民的Coord转换到对应的TransformComponent上。
	/// </summary>
	public class VillCoordToTransSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {}
	} 
}