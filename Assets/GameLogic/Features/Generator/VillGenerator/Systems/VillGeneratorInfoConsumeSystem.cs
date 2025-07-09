using GameLogic.Features.Generator;
using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// VillGeneratorInfoConsumeSystem 负责消费村民生成信息。
	/// </summary>
	public class VillGeneratorInfoConsumeSystem : ISystem {
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
			var geRes = _world.GetResource<VillGeneratorResource>();
			geRes.VillGenerateInfos.Clear();
		}
		public void OnRenderUpdate(float _) { }
	} 
}