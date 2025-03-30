namespace GameLogic
{
	public sealed class MapMgr : IClearMgr {
		private MapMgr() {}
		public static MapMgr Inst { get; } = new();

		public void ClearMgr() { }
	}
}