using NsEcsFrame.Core;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.WorldVill {
	public class VillCardFactory : MonoSingleton<VillCardFactory> {
		[SerializeField] private GameObject _villCardPrefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_villCardPrefab, 25);
		}
		public VillCard Create(Entity entity) {
			var card = PoolSystem.PopGO<VillCard>(_villCardPrefab);
			card.SetEntity(entity);
			return card;
		}
	}
}