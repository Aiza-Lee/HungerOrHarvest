using GameLogic.Model.Element.Arch;
using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillGroupFactory : MonoSingleton<VillGroupFactory> {
		[SerializeField] private GameObject _villGroupPrefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_villGroupPrefab, 15);
		}
		public VillGroup Create(ArchLogicBase archLogic) { 
			var vg = PoolSystem.PopGO<VillGroup>(_villGroupPrefab); 
			vg.SetGroupInfo(archLogic);
			return vg;
		}
		public VillGroup Create(GroupType groupType) { 
			var vg = PoolSystem.PopGO<VillGroup>(_villGroupPrefab);
			vg.SetGroupInfo(groupType);
			return vg;
		}
	}
}