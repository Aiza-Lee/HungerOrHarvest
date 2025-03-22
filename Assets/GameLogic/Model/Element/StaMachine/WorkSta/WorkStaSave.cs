namespace GameLogic 
{
	[System.Serializable]
	public class WorkStaSave : StaSaveBase {
		protected override StaSaveBase GetDerivedClone() {
			return new WorkStaSave();
		}
	}
}