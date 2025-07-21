using GameLogic.Features.Events;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	/// <summary>
	/// ArchTickProdSystem 负责处理"Arch的Tick生产逻辑"。
	/// </summary>
	public class ArchTickProdSystem : ISystem {
		public int Priority => 1000;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _archQuery;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_archQuery = _world.CreateQueryBuilder().WithAll<ArchIdentityComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_archQuery.Build().ForEach(arch => {
				var lconfig = ArchQueryAPI.GetLevelConfig(arch);
				arch.AddComponent<ArchTryProdRequestComponent>(new() {
					Cons = lconfig.SelfConsPerTick.ToNewEtList(),
					Prod = lconfig.SelfProdPerTick.ToNewEtList()
				});
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}