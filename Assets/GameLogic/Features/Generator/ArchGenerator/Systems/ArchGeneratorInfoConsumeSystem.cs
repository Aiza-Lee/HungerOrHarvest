using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// ArchGeneratorInfoConsumeSystem 负责消耗生成/销毁建筑的信息。
	/// </summary>
	public class ArchGeneratorInfoConsumeSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var geRes = _world.GetResource<ArchGeneratorResource>();
			geRes.ArchGenerateInfos.Clear();
		}
		public void OnRenderUpdate(float _) { }
	} 
}