using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Events;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// 处理和vill绑定的请求
	/// </summary>
	public class BondToVillSystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _queryBuilder;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_queryBuilder = world.CreateQueryBuilder().WithAll<BondToVillRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_queryBuilder.Build().ForEach(entity => {
				var request = entity.GetComponent<BondToVillRequestComponent>();
				var bond = entity.GetComponent<BondToVillComponent>();
				bond.BondedVillGids.Add(request.VillGid);
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}