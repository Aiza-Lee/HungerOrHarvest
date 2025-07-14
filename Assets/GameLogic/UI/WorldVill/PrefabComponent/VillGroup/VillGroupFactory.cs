using NsEcsFrame.Core;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.WorldVill {
	public class VillGroupFactory : MonoSingleton<VillGroupFactory> {
		[SerializeField] private GameObject _villGroupPrefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_villGroupPrefab, 15);
		}
		public VillGroup Create(Entity arch) {
			var vg = PoolSystem.PopGO<VillGroup>(_villGroupPrefab);
			vg.SetGroupInfo(arch);
			return vg;
		}
		public VillGroup Create(GroupType groupType) {
			var vg = PoolSystem.PopGO<VillGroup>(_villGroupPrefab);
			vg.SetGroupInfo(groupType);
			return vg;
		}
	}
}