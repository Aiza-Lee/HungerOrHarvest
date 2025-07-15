using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Events;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// 处理和vill绑定/解绑的请求
	/// </summary>
	public class BondToVillSystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _bondQuery, _disbondQuery;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_bondQuery = world.CreateQueryBuilder().WithAll<BondToVillRequestComponent>();
			_disbondQuery = world.CreateQueryBuilder().WithAll<DisbondVillRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_bondQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<BondToVillRequestComponent>();
				var bond = entity.GetComponent<BondToVillComponent>();
				if (bond.BondedVillGids.Contains(request.VillGid)) {
					Debug.LogWarning($"Vill Gid: {request.VillGid} is already bonded to Entity {entity}");
					return;
				}
				bond.BondedVillGids.Add(request.VillGid);
			});
			_disbondQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<DisbondVillRequestComponent>();
				var bond = entity.GetComponent<BondToVillComponent>();
				if (!bond.BondedVillGids.Contains(request.VillGid)) {
					Debug.LogWarning($"Vill Gid: {request.VillGid} is not bonded to Entity {entity}");
					return;
				}
				bond.BondedVillGids.Remove(request.VillGid);
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}