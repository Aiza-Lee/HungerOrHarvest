using GameLogic.Model.Mgr;
using NSFrame;

namespace GameLogic.View
{
	public class ViewConstMgr : MonoSingleton<ViewConstMgr> {

		public static readonly int MAX_SORTING_ORDER = 1000;
		public static readonly int FRONT_SORTING_ORDER = 5;
		public static readonly int VILL_SORTING_ORDER = 4;
		public static readonly int ARCH_SORTING_ORDER = 3;
		public static readonly int EARTH_SORTING_ORDER = 2;
		public static readonly int BACK_SORTING_ORDER = 1;

		/// <summary>
		/// view的X轴与model的X轴的比例
		/// </summary>
		public static readonly float VX_MX_RATE = 0.4f;
		/// <summary>
		/// view的Z轴与model的Y轴的比例
		/// </summary>
		public static readonly float VZ_MY_RATE = 0.4f;
		/// <summary>
		/// view的Y轴的默认值（地平线的高度）
		/// </summary>
		public static readonly float DEFAULT_Y = 0f;
		public static ViewConstConfig GetConfig => Inst.Config;
		public static float LayerGap => ConstMgr.Y_PER_LYR * VZ_MY_RATE;
		public ViewConstConfig Config;
	}
}