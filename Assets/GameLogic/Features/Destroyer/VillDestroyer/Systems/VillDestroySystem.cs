using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// VillDestroyerSystem 负责销毁村民实体
	/// </summary>
	public class VillDestroyerSystem : ISystem {
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
			var res = _world.GetResource<VillDestroyResource>();
			var toDestroy = res.VillToDestroy;
			foreach (var gid in toDestroy) {
				var entity = GameWorldMono.GidToEntity[gid];

				// 创建删除事件实体
				var eventEntity = _world.CreateEntity();
				eventEntity.AddComponent(new VillDestroyedEventComp_Logic() { DestroyedVillGid = gid });

				// 解除和建筑的绑定
				ClearBond(entity, gid);

				EntityDestroyUtil.DestroyEntity(entity.ID);
			}
			toDestroy.Clear();
		}
		public void OnRenderUpdate(float _) { }

		private void ClearBond(Entity entity, ulong gid) {
			var archBond = entity.GetComponent<BondToArchComponent>();
			if (archBond.HomeArchGid != 0) {
				var arch = GameWorldMono.GidToEntity[archBond.HomeArchGid];
				var bondVills = arch.GetComponent<BondToVillComponent>();
				bondVills.BondedVillGids.Remove(gid);
			}
			if (archBond.WorkArchGid != 0) {
				var arch = GameWorldMono.GidToEntity[archBond.WorkArchGid];
				var bondVills = arch.GetComponent<BondToVillComponent>();
				bondVills.BondedVillGids.Remove(gid);
			}
		}
	} 
}