using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Model.Mgr
{
	/// <summary>
	/// Model层的配置文件总和，包含了所有Model层需要的配置信息
	/// </summary>
	[CreateAssetMenu(fileName = "ConstConfig", menuName = "HungerOrHarvest/Config/ConstConfig")]
	public class ModelConfig : ScriptableObject {

		public ulong DAY_TICKS;
		public ulong NIGHT_TICKS;
		public float VILL_ONE_MOVE_TICK;
		public float NIGHT_TIME_SPEED;

		[Header("务必为每一种职业、建筑、村民创建配置, 否则程序无法正常运行")]
		[Space]
		[Space]
		[SerializeField] private List<JobConfig> JobConfigs;
		[SerializeField] private List<ArchConfigBase> ArchConfigs;
		[SerializeField] private List<VillConfigBase> VillConfigs;

		private readonly List<Pair<JobType, JobConfig>> _jobConfigs = new();
		private readonly List<Pair<ArchType, ArchConfigBase>> _archConfigs = new();
		private readonly List<Pair<VillType, VillConfigBase>> _villConfigs = new();

		// 这里注释掉的是防止配置不全导致不能直接使用下标，但是懒得写了
		// private Dictionary<ArchType, ArchConfigBase> _archConfigs;

		public void SetConfig() {
			// _archConfigs = new();
			// ArchConfigs.ForEach((pair) => _archConfigs.Add(pair.Key, pair.Value));

			if (JobConfigs.Count != ConstMgr.JOB_TYPE_SIZE) {
				Debug.LogError("职业配置数量不正确");
				return;
			}
			if (ArchConfigs.Count != ConstMgr.ARCH_TYPE_SIZE) {
				Debug.LogError("建筑配置数量不正确");
				return;
			}
			if (VillConfigs.Count != ConstMgr.VILL_TYPE_SIZE) {
				Debug.LogError("村民配置数量不正确");
				return;
			}

			JobConfigs.ForEach((config) => _jobConfigs.Add(new(config.JobType, config)));
			ArchConfigs.ForEach((config) => _archConfigs.Add(new(config.ArchType, config)));
			VillConfigs.ForEach((config) => _villConfigs.Add(new(config.VillType, config)));

			_jobConfigs.Sort(new PairComparer<JobType, JobConfig>());
			_archConfigs.Sort(new PairComparer<ArchType, ArchConfigBase>());
			_villConfigs.Sort(new PairComparer<VillType, VillConfigBase>());
		}
		/// <summary>
		/// 按照枚举排序，查找的时候就可以用下标直接访问
		/// </summary>
		private sealed class PairComparer<K, V> : IComparer<Pair<K, V>> {
			public int Compare(Pair<K, V> x, Pair<K, V> y) {
				return Convert.ToInt32(x.Key).CompareTo(Convert.ToInt32(y.Key));
			}
		}

		#region PublicMethods
		public ArchConfigBase FindArchConfig(ArchType type) => _archConfigs[(int) type].Value;
		public ArchConfigBase FindArchConfig(int index) => _archConfigs[index].Value;
		public JobConfig FindJobConfig(JobType type) => _jobConfigs[(int) type].Value;
		public JobConfig FindJobConfig(int index) => _jobConfigs[index].Value;
		public VillConfigBase FindVillConfig(VillType type) => _villConfigs[(int) type].Value;
		public VillConfigBase FindVillConfig(int index) => _villConfigs[index].Value;
		#endregion
	}
}