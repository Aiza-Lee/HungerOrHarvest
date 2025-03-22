namespace GameLogic
{
	public sealed class MapMgr {
		private MapMgr() {}
		public static MapMgr Inst { get; } = new();
	}
}