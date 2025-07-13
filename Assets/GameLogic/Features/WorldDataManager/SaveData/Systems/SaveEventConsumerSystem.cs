using NsEcsFrame.Core;

namespace GameLogic.Features.WorldDataManager {
	/// <summary>
	/// SaveEventComsumerSystem 负责消耗保存事件。
	/// </summary>
	public class SaveEventConsumerSystem_Logic : ISystem {
		public int Priority => 2000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_world.CreateQueryBuilder()
				.WithAll<SaveEventComponent_Logic>()
				.Build()
				.ForEach(e => e.Destroy());
		}
		public void OnRenderUpdate(float _) { }
	} 
}