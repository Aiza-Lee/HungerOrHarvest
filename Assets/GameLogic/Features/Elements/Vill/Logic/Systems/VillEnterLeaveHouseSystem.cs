using GameLogic.Features.Events;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// 处理村民进出房屋的请求
	/// </summary>
	public class VillEnterLeaveArchSystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _enterHomeQuery, _enterWorkArchQuery, _leaveArchQuery;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_enterHomeQuery = _world.CreateQueryBuilder().WithAll<VillEnterHomeArchRequestComponent>();
			_enterWorkArchQuery = _world.CreateQueryBuilder().WithAll<VillEnterWorkArchRequestComponent>();
			_leaveArchQuery = _world.CreateQueryBuilder().WithAll<VillLeaveArchRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_enterHomeQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<VillEnterHomeArchRequestComponent>();
				var inArchComp = entity.GetComponent<InArchComponent>();
				var home = VillQueryAPI.GetHomeArchGid(entity);
				inArchComp.ArchGid = home;
			});
			_enterWorkArchQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<VillEnterWorkArchRequestComponent>();
				var inArchComp = entity.GetComponent<InArchComponent>();
				var workArch = VillQueryAPI.GetWorkArchGid(entity);
				inArchComp.ArchGid = workArch;
			});
			_leaveArchQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<VillLeaveArchRequestComponent>();
				var inArchComp = entity.GetComponent<InArchComponent>();
				inArchComp.ArchGid = 0;
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}