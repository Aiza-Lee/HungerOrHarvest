using NsEcsFrame.Core;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// LayerGeneratorInfoConsumeSystem 负责消耗 LayerGeneratorInfo 资源。
	/// </summary>
	public class LayerGeneratorInfoConsumeSystem : ISystem {
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
			var geRes = _world.GetResource<LayerGeneratorResource>();
			geRes.LayerDatas.Clear();
		}
		public void OnRenderUpdate(float _) { }
	} 
}