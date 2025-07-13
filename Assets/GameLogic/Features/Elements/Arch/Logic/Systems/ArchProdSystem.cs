using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	/// <summary>
	/// ArchProdSystem 负责建筑本身的产出和消耗
	/// </summary>
	public class ArchProdSystem : ISystem {
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
			var query = _world.CreateQueryBuilder().WithAll<ArchIdentityComponent>().Build();
			var configs = _world.GetResource<ArchConfigResource>();
			query.ForEach(entity => {
				var type = entity.GetComponent<ArchIdentityComponent>().ArchType;
				var config = configs.GetConfig(type);
				var level = ArchUtils.ArchLevel(entity);
				var lConfig = config.LevelConfigs[level];

				var repoRes = _world.GetResource<>();
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}