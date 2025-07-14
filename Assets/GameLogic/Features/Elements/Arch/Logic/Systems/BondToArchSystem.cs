using GameLogic.Common.DataTypes;
using GameLogic.Common.Utils;
using GameLogic.Features.Events;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	/// <summary>
	/// 负责处理和arch绑定的请求
	/// </summary>
	public class BondToArchSystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _queryBuilder;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_queryBuilder = world.CreateQueryBuilder().WithAll<BondToArchRequestComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_queryBuilder.Build().ForEach(entity => {
				var request = entity.GetComponent<BondToArchRequestComponent>();
				var bond = entity.GetComponent<BondToArchComponent>();
				if (request.ArchGid.ToEntity().GetComponent<ArchIdentityComponent>().ArchType == ArchType.Cottage) {
					bond.HomeArchGid = request.ArchGid;
				} else {
					bond.WorkArchGid = request.ArchGid;
				}
			});
		}
		public void OnRenderUpdate(float _) { }
	}
}