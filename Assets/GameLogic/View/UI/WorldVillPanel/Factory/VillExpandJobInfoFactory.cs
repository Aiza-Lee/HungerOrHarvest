using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillExpandJobInfoFactory : MonoSingleton<VillExpandJobInfoFactory> {
		[SerializeField] private GameObject _prefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_prefab, 15);
		}
		public VillExpandJobInfo Create(ulong villID, JobType jobType) {
			var info = PoolSystem.PopGO<VillExpandJobInfo>(_prefab);
			info.InjectVillAndJobType(WorldMgr.Inst.FindVill(villID), jobType);
			return info;
		}
	}
}