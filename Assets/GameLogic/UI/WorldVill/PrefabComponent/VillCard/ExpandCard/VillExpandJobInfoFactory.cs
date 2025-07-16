using NSFrame;
using UnityEngine;

namespace GameLogic.UI.WorldVill {
	public class VillExpandJobInfoFactory : MonoSingleton<VillExpandJobInfoFactory> {
		[SerializeField] private GameObject _prefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_prefab, 15);
		}
		public VillExpandJobInfo Create() {
			var info = PoolSystem.PopGO<VillExpandJobInfo>(_prefab);
			return info;
		}
	}
}