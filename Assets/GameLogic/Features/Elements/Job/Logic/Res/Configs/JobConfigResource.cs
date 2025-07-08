using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Job {
	public class JobConfigResource : IResource {
		[SerializeReference][SerializeField][Tooltip("没有找到配置时的默认配置")] private JobConfigBase DefaultConfig;
		[SerializeReference][SerializeField][Tooltip("没有找到配置时的默认配置")] private JobArtConfigBase DefaultArtConfig;
		[SerializeReference][SerializeField] private List<JobConfigBase> Configs = new();
		[SerializeReference][SerializeField] private List<JobArtConfigBase> ArtConfigs = new();
		private Dictionary<JobType, JobConfigBase> _configs;
		private Dictionary<JobType, JobArtConfigBase> _artConfigs;

		public JobConfigBase GetConfig(JobType jobType) {
			if (_configs == null) {
				_configs = new Dictionary<JobType, JobConfigBase>();
				foreach (var c in Configs) {
					_configs[c.JobType] = c;
				}
			}
			return _configs.TryGetValue(jobType, out var config) ? config : DefaultConfig;
		}

		public JobArtConfigBase GetArtConfig(JobType jobType) {
			if (_artConfigs == null) {
				_artConfigs = new Dictionary<JobType, JobArtConfigBase>();
				foreach (var c in ArtConfigs) {
					_artConfigs[c.JobType] = c;
				}
			}
			return _artConfigs.TryGetValue(jobType, out var artConfig) ? artConfig : DefaultArtConfig;
		}
	}

	public abstract class JobConfigBase : ScriptableObject {
		public abstract JobType JobType { get; }
		public string JobName;
		public string JobDescription;
		[Tooltip("职业等级配置")] public List<JobLevelConfigBase> LevelConfigs;
	}
	public abstract class JobLevelConfigBase : ScriptableObject {
		[Tooltip("等级")] public int Level;
		[Tooltip("升到下一级所需经验值（升级会扣除所需经验值）")] public float NextLevelExpDemand;
		[Tooltip("消耗减免的增量")] public ReadOnlyEtList<RepoType, float> RepoConsBuffSave;
		[Tooltip("产出增益的增量")] public ReadOnlyEtList<RepoType, float> RepoProdBuffSave;
	}

	public abstract class JobArtConfigBase : ScriptableObject {
		public abstract JobType JobType { get; }
		[Tooltip("Job精灵")] public Sprite Sprite;
	}
}