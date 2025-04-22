using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillCardFactory : MonoSingleton<VillCardFactory> {
		[SerializeField] private GameObject _villCardPrefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_villCardPrefab, 25);
		}
		public VillCard Create(ulong villID) { 
			var villCard = PoolSystem.PopGO<VillCard>(_villCardPrefab); 
			villCard.InjectVillView(WorldViewMgr.Inst.FindVillView(villID));
			return villCard;
		}
	}
}