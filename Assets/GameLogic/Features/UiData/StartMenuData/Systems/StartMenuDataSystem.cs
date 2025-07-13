using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.UiData.StartMenuData {
	/// <summary>
	/// StartMenuDataSystem 负责管理开始菜单数据
	/// </summary>
	public class StartMenuDataSystem : ISystem {
		public int Priority => 350;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var query = _world.CreateQueryBuilder().WithAll<SaveEventComponent_Logic>().Build();
			if (query.Count == 0) return;
			var startMenuRes = _world.GetResource<StartMenuDataResource>();
			startMenuRes.IsAnySaveChanged = true;
		}
		public void OnRenderUpdate(float _) { }
	} 
}