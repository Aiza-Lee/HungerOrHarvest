namespace GameLogic
{
	[System.Serializable]
	public class RepoMgrSave {
		public RTList<float> Repos;
		public RTList<float> GlobalConsBuffs;
		public RTList<float> GlobalProdBuffs;
		public RTList<bool> UnlockedRepos;
	}
}