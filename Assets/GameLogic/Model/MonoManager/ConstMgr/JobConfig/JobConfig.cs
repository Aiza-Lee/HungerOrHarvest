using System.Collections.Generic;
using UnityEngine;

namespace GameLogic 
{
	[CreateAssetMenu(fileName = "JobConfig", menuName = "HungerOrHarvest/Config/Job/Job")]
	public class JobConfig : ScriptableObject {
		[Header("职业种类")] public JobType JobType;
		[Header("中文名称")] public string ChineseName;
		[Header("每级配置")] public List<JobLevelConfig> JobLevelConfigs;
	}
}