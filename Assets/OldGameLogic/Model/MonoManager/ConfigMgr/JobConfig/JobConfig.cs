using System;
using System.Collections.Generic;
using UnityEngine;

namespace OldGameLogic.Model.Mgr
{
	// [CreateAssetMenu(fileName = "JobConfig", menuName = "HungerOrHarvest/Config/Job/Job")]
	public class JobConfig : ScriptableObject {
		[Header("类型名称(区分大小写)")] public string TypeName;
		private JobType? _jobType = null;
		public JobType JobType => _jobType ??= Enum.Parse<JobType>(TypeName);
		
		[Header("中文名称")] public string ChineseName;
		[Header("每级配置")] public List<JobLevelConfig> JobLevelConfigs;
	}
}