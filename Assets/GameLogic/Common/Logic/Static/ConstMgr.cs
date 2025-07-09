using GameLogic.Common.DataTypes;
using System;
using UnityEngine;

namespace GameLogic.Common.Logic {
	/// <summary>
	/// 关乎游戏能否正常运行，修改需要谨慎的常量管理器。
	/// </summary>
	public static class ConstMgr {
		public static readonly int ARCH_TYPE_SIZE;
		public static readonly int JOB_TYPE_SIZE;
		public static readonly int LAYER_TYPE_SIZE;
		public static readonly int REPO_TYPE_SIZE;
		public static readonly int VILL_TYPE_SIZE;

		public static readonly int LAYER_CAPACITY = 50;         // 每层的容量
		public static readonly int X_PER_ODR = 12;              // 保证为偶数
		public static readonly int Y_PER_LYR = 24;
		public static readonly int LAYERS = 21;                 // 总层数上限
		public static readonly int MAX_LYR = (LAYERS - 1) / 2;  // 最大层编号
		public static readonly int MIN_LYR = -MAX_LYR;          // 最小层编号

		public static readonly int MAX_SORTING_ORDER = 1000;
		public static readonly int FRONT_SORTING_ORDER = 5;
		public static readonly int VILL_SORTING_ORDER = 4;
		public static readonly int ARCH_SORTING_ORDER = 3;
		public static readonly int EARTH_SORTING_ORDER = 2;
		public static readonly int BACK_SORTING_ORDER = 1;

		public static readonly float VX_MX_RATE = 0.4f;         // view的X轴与model的X轴的比例
		public static readonly float VZ_MY_RATE = 0.4f;         // view的Z轴与model的Y轴的比例
		public static float DEFAULT_Y = 0f;            			// view的Y轴的默认值（地平线的高度）
		public static float LayerGap => Y_PER_LYR * VZ_MY_RATE;

		public static readonly uint Speedx1TicksPerSecond = 50; // 1倍速每秒的Tick数

		static ConstMgr() {
			ARCH_TYPE_SIZE = GetEnumSize<ArchType>();
			JOB_TYPE_SIZE = GetEnumSize<JobType>();
			LAYER_TYPE_SIZE = GetEnumSize<LayerType>();
			REPO_TYPE_SIZE = GetEnumSize<RepoType>();
			VILL_TYPE_SIZE = GetEnumSize<VillType>();
		}
		private static int GetEnumSize<T>() where T : Enum {
			return Enum.GetValues(typeof(T)).Length;
		}
	}

	public static class ConstMgrExt {
		public static Vector3 ToVec3(this Coord coord, int y) {
			return new Vector3(
				coord.X * ConstMgr.VX_MX_RATE,
				y,
				coord.Y * ConstMgr.VZ_MY_RATE);
		}
		public static Vector3 ToVec3DefaultY(this Coord coord) {
			return new Vector3(
				coord.X * ConstMgr.VX_MX_RATE,
				ConstMgr.DEFAULT_Y,
				coord.Y * ConstMgr.VZ_MY_RATE);
		}
	}
}