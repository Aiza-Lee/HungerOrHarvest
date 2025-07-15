using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	/// <summary>
	/// 负责消耗事件
	/// </summary>
	public class LogicFrameRequestConsumeSystem : ISystem {
		public int Priority => 2000;
		public bool Enabled { get; set; }
		private IWorld _world;

		private EntityQueryBuilder _VillTryProd, _VillCostVit, _ExpGain, _VitGain, _VitConsFoodRecoverVit;
		private EntityQueryBuilder _ArchtryProd;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			InitializeQueries();
		}
		private void InitializeQueries() {
			// Vill
			_VillTryProd = _world.CreateQueryBuilder().WithAll<VillTryProdRequestComponent>();
			_VillCostVit = _world.CreateQueryBuilder().WithAll<VillCostVitRequestComponent>();
			_ExpGain = _world.CreateQueryBuilder().WithAll<ExpGainRequestComponent>();
			_VitGain = _world.CreateQueryBuilder().WithAll<VillGainVitRequestComponent>();
			_VitConsFoodRecoverVit = _world.CreateQueryBuilder().WithAll<VillConsFoodRecoverVitRequestComponent>();
			// Arch
			_ArchtryProd = _world.CreateQueryBuilder().WithAll<VillTryProdRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			ConsumeVillAbout();
			ConsumeArchAbout();
		}
		private void ConsumeVillAbout() {
			_VillTryProd.Build().ForEach(entity => entity.RemoveComponent<VillTryProdRequestComponent>());
			_VillCostVit.Build().ForEach(entity => entity.RemoveComponent<VillCostVitRequestComponent>());
			_ExpGain.Build().ForEach(entity => entity.RemoveComponent<ExpGainRequestComponent>());
			_VitGain.Build().ForEach(entity => entity.RemoveComponent<VillGainVitRequestComponent>());
			_VitConsFoodRecoverVit.Build().ForEach(entity => entity.RemoveComponent<VillConsFoodRecoverVitRequestComponent>());
		}
		private void ConsumeArchAbout() {
			_ArchtryProd?.Build().ForEach(entity => entity.RemoveComponent<VillTryProdRequestComponent>());
		}
		public void OnRenderUpdate(float _) { }
	}
}