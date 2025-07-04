using System.Collections.Generic;

namespace OldGameLogic
{
	[System.Serializable]
	public class RepoMgrSave {
		public RTListSave<float> Repos;
		public RTListSave<float> GlobalConsBuffs;
		public RTListSave<float> GlobalProdBuffs;
		public RTListSave<bool> UnlockedRepos;
		public RTListSave<float> DailyCons;
		public RTListSave<float> DailyProd;
		public RTListSave<float> LastSecondNet;
		public List<RTListSave<float>> LastSecondTickProduces;
	}
}