namespace OldGameLogic
{
	public sealed class MapMgr : IMananger {
		private MapMgr() {}
		public static MapMgr Inst { get; } = new();

		public void ClearMgr() { }
	}
}