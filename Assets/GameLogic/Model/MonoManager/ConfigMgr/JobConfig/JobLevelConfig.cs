using UnityEngine;

namespace GameLogic.Model.Mgr
{
	
	[CreateAssetMenu(fileName = "JobLevelConfig_x", menuName = "HungerOrHarvest/Config/Job/Level")]
	public class JobLevelConfig : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("升到下一级所需经验")] public float LevelUpDemand;
		[Header("消耗减免的增量")] public RTListSave<float> RepoConsBuffSave;
		[Header("产出增益的增量")] public RTListSave<float> RepoProdBuffSave;

		private RTList<float> _repoConsBuff;
		public RTList<float> RepoConsBuff {
			get {
				if (_repoConsBuff == null) {
					_repoConsBuff = new();
					_repoConsBuff.InitFromSave(RepoConsBuffSave);
				}
				return _repoConsBuff;
			}
		}
		private RTList<float> _repoProdBuff;
		public RTList<float> RepoProdBuff {
			get {
				if (_repoProdBuff == null) {
					_repoProdBuff = new();
					_repoProdBuff.InitFromSave(RepoProdBuffSave);
				}
				return _repoProdBuff;
			}
		}

	}
}