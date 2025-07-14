using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// ArchDestroyerSystem 负责销毁建筑实体。
	/// </summary>
	public class ArchDestroyerSystem : ISystem {
		public int Priority => 500;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var res = _world.GetResource<ArchDestroyResource>();
			var gids = res.ArchToDestroyGid;
			foreach (var gid in gids) {
				var entity = GameWorldMono.GidToEntity[gid];

				// 创建删除事件实体
				var eventEntity = _world.CreateEntity();
				eventEntity.AddComponent(new ArchDestroyedEventComp_Logic() { ArchGid = gid });

				// 解除和村民的绑定
				ClearBond(entity);

				EntityDestroyUtil.DestroyEntity(entity.ID);
			}
			gids.Clear();
		}
		public void OnRenderUpdate(float _) { }

		private void ClearBond(Entity entity) {
			var archType = entity.GetComponent<ArchIdentityComponent>().ArchType;
			var villBond = entity.GetComponent<BondToVillComponent>();
			villBond.BondedVillGids.ForEach(gid => {
				var vill = GameWorldMono.GidToEntity[gid];
				var bondArch = vill.GetComponent<BondToArchComponent>();
				if (archType == ArchType.Cottage) {
					bondArch.HomeArchGid = 0;
				} else {
					bondArch.WorkArchGid = 0;
				}
			});
		}
	} 
}