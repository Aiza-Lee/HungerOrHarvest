using System.Collections.Generic;

namespace GameLogic
{
	[System.Serializable]
	public class RepoMgrSave {
		public RTList<float> Repos;
		public RTList<float> GlobalConsBuffs;
		public RTList<float> GlobalProdBuffs;
		public RTList<bool> UnlockedRepos;
		public RTList<float> DailyCons;
		public RTList<float> DailyProd;
		public RTList<float> LastSecondNet;
		public List<RTList<float>> LastSecondTickProduces;
	}
}