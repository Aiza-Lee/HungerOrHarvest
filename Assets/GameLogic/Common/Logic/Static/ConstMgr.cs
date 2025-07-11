using GameLogic.Common.DataTypes;
using System;
using UnityEngine;

namespace GameLogic.Common.Logic {
	/// <summary>关乎游戏能否正常运行，修改需要谨慎的常量管理器。</summary>
	public static class ConstMgr {
		public static readonly int ARCH_TYPE_SIZE;
		public static readonly int JOB_TYPE_SIZE;
		public static readonly int LAYER_TYPE_SIZE;
		public static readonly int REPO_TYPE_SIZE;
		public static readonly int VILL_TYPE_SIZE;

		/// <summary>每Order对应Coord的X轴长度</summary>
		public static readonly int CX_PER_ODR = 12;
		/// <summary>每Layer对应Coord的Y轴长度</summary>
		public static readonly int CY_PER_LYR = 24;

		/// <summary>最大Order，单位为OL</summary>
		public static readonly int MAX_ORDER = 50;
		/// <summary>最小Order，单位为OL</summary>
		public static readonly int MIN_ORDER = 0;
		/// <summary>中间Order，单位为OL</summary>
		public static readonly int MIDDLE_ORDER = (MAX_ORDER + MIN_ORDER) / 2;

		/// <summary>最大Layer，单位为OL</summary>
		public static readonly int MAX_LAYER = 20;
		/// <summary>最小Layer，单位为OL</summary>
		public static readonly int MIN_LAYER = 0;
		/// <summary>中间Layer，单位为OL</summary>
		public static readonly int MIDDLE_LAYER = (MAX_LAYER + MIN_LAYER) / 2;

		/// <summary>世界中心点，单位为OL</summary>
		public static readonly OL WORLD_CENTER_OL = new(MIDDLE_ORDER, MIDDLE_LAYER);
		/// <summary>世界中心点，单位为Coord</summary>
		public static readonly Coord WORLD_CENTER_COORD = WORLD_CENTER_OL.ToCoord();
		/// <summary>世界中心点，单位为Unity世界坐标系的Vec3</summary>
		public static readonly Vector3 WORLD_CENTER_VEC3 = WORLD_CENTER_OL.ToVec3DefaultY();

		public static readonly int MAX_SORTING_ORDER = 1000;
		public static readonly int FRONT_SORTING_ORDER = 5;
		public static readonly int VILL_SORTING_ORDER = 4;
		public static readonly int ARCH_SORTING_ORDER = 3;
		public static readonly int EARTH_SORTING_ORDER = 2;
		public static readonly int BACK_SORTING_ORDER = 1;

		/// <summary>单位Coord的X，在Unity世界坐标系中X轴的长度</summary>
		public static readonly float UX_PER_CX = 0.4f;
		/// <summary>单位Coord的Y，在Unity世界坐标系中Z轴的长度</summary>
		public static readonly float UZ_PER_CY = 0.4f;
		/// <summary>地平线的高度，单位为Unity世界坐标系的Y轴</summary>
		public static float DEFAULT_Y = 0f;
		public static float LayerGap => CY_PER_LYR * UZ_PER_CY;
		/// <summary>1倍速下每秒的Tick数</summary>
		public static readonly uint SPEEDx1_TICKS_PER_SECOND = 50;

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
		public static Vector3 ToVec3(this Coord coord, float y) {
			return new Vector3(
				coord.X * ConstMgr.UX_PER_CX,
				y,
				coord.Y * ConstMgr.UZ_PER_CY);
		}
		public static Vector3 ToVec3DefaultY(this Coord coord) {
			return new Vector3(
				coord.X * ConstMgr.UX_PER_CX,
				ConstMgr.DEFAULT_Y,
				coord.Y * ConstMgr.UZ_PER_CY);
		}
		public static Vector3 ToVec3(this OL ol, float y) {
			return new Vector3(
				ConstMgr.UX_PER_CX * ConstMgr.CX_PER_ODR * ol.ODR,
				y,
				ConstMgr.UZ_PER_CY * ConstMgr.CY_PER_LYR * ol.LYR);
		}
		public static Vector3 ToVec3DefaultY(this OL ol) {
			return new Vector3(
				ConstMgr.UX_PER_CX * ConstMgr.CX_PER_ODR * ol.ODR,
				ConstMgr.DEFAULT_Y,
				ConstMgr.UZ_PER_CY * ConstMgr.CY_PER_LYR * ol.LYR);
		}
	}
}