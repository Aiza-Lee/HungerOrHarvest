using NSFrame;

namespace GameLogic
{
	public class ViewConstMgr : MonoSingleton<ViewConstMgr> {
		public static readonly int MAX_SORTING_ORDER = 1000;
		public static readonly int FRONT_SORTING_ORDER = 5;
		public static readonly int VILL_SORTING_ORDER = 4;
		public static readonly int ARCH_SORTING_ORDER = 3;
		public static readonly int EARTH_SORTING_ORDER = 2;
		public static readonly int BACK_SORTING_ORDER = 1;
		public ViewConstConfig Config;
	}
}