using NsEcsFrame.Core;

namespace GameLogic.Features.Events {
	/// <summary>
	/// 负责消耗事件
	/// </summary>
	public class LogicFrameRequestConsumeSystem : ISystem {
		public int Priority => 2000;
		public bool Enabled { get; set; }
		private IWorld _world;

		private EntityQueryBuilder _VillTryProd, _VillCostVit, _ExpGain, _VitGain, _VitConsFoodRecoverVit, _BondToArch, _DisbondArch;
		private EntityQueryBuilder _ArchtryProd;
		private EntityQueryBuilder _BondToVill, _DisbondVill, _EnterWorkArch, _EnterHome, _LeaveArch;

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
			_BondToArch = _world.CreateQueryBuilder().WithAll<BondToArchRequestComponent>();
			_DisbondArch = _world.CreateQueryBuilder().WithAll<DisbondArchRequestComponent>();
			// Arch
			_ArchtryProd = _world.CreateQueryBuilder().WithAll<VillTryProdRequestComponent>();
			// Vill Container
			_BondToVill = _world.CreateQueryBuilder().WithAll<BondToVillRequestComponent>();
			_DisbondVill = _world.CreateQueryBuilder().WithAll<DisbondVillRequestComponent>();
			_EnterWorkArch = _world.CreateQueryBuilder().WithAll<VillEnterWorkArchRequestComponent>();
			_EnterHome = _world.CreateQueryBuilder().WithAll<VillEnterHomeArchRequestComponent>();
			_LeaveArch = _world.CreateQueryBuilder().WithAll<VillLeaveArchRequestComponent>();

		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			ConsumeVillAbout();
			ConsumeArchAbout();
			ConsumeVillContainerAbout();
		}
		private void ConsumeVillAbout() {
			_VillTryProd.Build().ForEach(entity => entity.RemoveComponent<VillTryProdRequestComponent>());
			_VillCostVit.Build().ForEach(entity => entity.RemoveComponent<VillCostVitRequestComponent>());
			_ExpGain.Build().ForEach(entity => entity.RemoveComponent<ExpGainRequestComponent>());
			_VitGain.Build().ForEach(entity => entity.RemoveComponent<VillGainVitRequestComponent>());
			_VitConsFoodRecoverVit.Build().ForEach(entity => entity.RemoveComponent<VillConsFoodRecoverVitRequestComponent>());
			_BondToArch.Build().ForEach(entity => entity.RemoveComponent<BondToArchRequestComponent>());
			_DisbondArch.Build().ForEach(entity => entity.RemoveComponent<DisbondArchRequestComponent>());
		}
		private void ConsumeArchAbout() {
			_ArchtryProd?.Build().ForEach(entity => entity.RemoveComponent<VillTryProdRequestComponent>());
		}
		private void ConsumeVillContainerAbout() {
			_BondToVill?.Build().ForEach(entity => entity.RemoveComponent<BondToVillRequestComponent>());
			_DisbondVill?.Build().ForEach(entity => entity.RemoveComponent<DisbondVillRequestComponent>());
			_EnterWorkArch?.Build().ForEach(entity => entity.RemoveComponent<VillEnterWorkArchRequestComponent>());
			_EnterHome?.Build().ForEach(entity => entity.RemoveComponent<VillEnterHomeArchRequestComponent>());
			_LeaveArch?.Build().ForEach(entity => entity.RemoveComponent<VillLeaveArchRequestComponent>());
		}

		public void OnRenderUpdate(float _) { }
	}
}