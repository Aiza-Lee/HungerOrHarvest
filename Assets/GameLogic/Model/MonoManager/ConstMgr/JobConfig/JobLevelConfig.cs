using UnityEngine;

namespace GameLogic
{
	
	[CreateAssetMenu(fileName = "JobLevelConfig_x", menuName = "HungerOrHarvest/Config/Job/Level")]
	public class JobLevelConfig : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("升到下一级所需经验")] public float LevelUpDemand;
		[Header("消耗减免的增量")] public RTList<float> RepoConsBuff;
		[Header("产出增益的增量")] public RTList<float> RepoProdBuff;

	}
}