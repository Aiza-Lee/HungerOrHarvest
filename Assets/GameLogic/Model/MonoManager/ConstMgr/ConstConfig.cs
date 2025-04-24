using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "ConstConfig", menuName = "HungerOrHarvest/Config/ConstConfig")]
	public class ConstConfig : ScriptableObject {

		public ulong DAY_TICKS;
		public ulong NIGHT_TICKS;
		public float VILL_ONE_MOVE_TICK;
		public float NIGHT_TIME_SPEED;

		[Header("职业配置请务必完善每一种职业的配置信息,内部实现要求")]
		public List<Pair<JobType, JobConfig>> JobConfigs;
		public List<Pair<ArchType, ArchConfigBase>> ArchConfigs;

		private Dictionary<ArchType, ArchConfigBase> _archConfigs;

		public void SetConfig() {
			_archConfigs = new();
			ArchConfigs.ForEach( (pair) => _archConfigs.Add(pair.Key, pair.Value) );
			JobConfigs.Sort(PairComparer.Inst);

		}
		private sealed class PairComparer : IComparer<Pair<JobType, JobConfig>> {
			private PairComparer() {}
			public static PairComparer Inst { get; } = new();
			public int Compare(Pair<JobType, JobConfig> x, Pair<JobType, JobConfig> y) {
				return ((int)x.Key).CompareTo((int)y.Key);
			}
		}

		#region PublicMethods
		public ArchConfigBase FindArchConfig(ArchType type) => _archConfigs[type];
		public JobConfig FindJobConfig(JobType type) => JobConfigs[(int)type].Value;
		public JobConfig FindJobConfig(int index) => JobConfigs[index].Value;

		#endregion
	}
}