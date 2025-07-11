using NsEcsFrame.Core;

namespace GameLogic.Features.NewWorldCreator {
	/// <summary>
	/// NewWorldCreatorSystem 负责创建新世界的逻辑。
	/// </summary>
	public class NewWorldCreatorSystem : ISystem {
		public int Priority => -100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var info = _world.GetResource<NewWorldInfoResource>().NewWorldInfo;
			if (info == null) return;
			var baseInfo = info.BaseInfo;
			var worldName = info.WorldName;
			ClearWorld.ClearWorldAPI.Clear();
			_world.Name = worldName;
			
		}
		public void OnRenderUpdate(float _) { }
	} 
}