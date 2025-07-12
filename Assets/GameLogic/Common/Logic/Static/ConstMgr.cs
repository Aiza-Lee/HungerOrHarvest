using GameLogic.Common.DataTypes;
using NsEcsFrame.Unity;
using System;
using System.Collections.Generic;
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
		public const int CX_PER_ODR = 12;
		/// <summary>每Layer对应Coord的Y轴长度</summary>
		public const int CY_PER_LYR = 24;

		/// <summary>最大Order，单位为OL</summary>
		public const int MAX_ORDER = 60;
		public static int MAX_CX => CX_PER_ODR * MAX_ORDER;
		public static float MAX_UX => UX_PER_CX * MAX_CX ;

		/// <summary>最小Order，单位为OL</summary>
		public const int MIN_ORDER = 0;
		public static int MIN_CX => CX_PER_ODR * MIN_ORDER;
		public static float MIN_UX => UX_PER_CX * MIN_CX;

		/// <summary>中间Order，单位为OL，保证为偶数</summary>
		public const int MIDDLE_ORDER = 30;

		/// <summary>最大Layer，单位为OL</summary>
		public const int MAX_LAYER = 20;
		public static int MAX_CY => CY_PER_LYR * MAX_LAYER;
		public static float MAX_UZ => UZ_PER_CY * MAX_CY;

		/// <summary>最小Layer，单位为OL</summary>
		public const int MIN_LAYER = 0;
		public static int MIN_CY => CY_PER_LYR * MIN_LAYER;
		public static float MIN_UZ => UZ_PER_CY * MIN_CY;

		/// <summary>中间Layer，单位为OL，保证为偶数</summary>
		public const int MIDDLE_LAYER = 10;

		/// <summary>世界中心点，单位为OL，保证OL都是偶数</summary>
		public static readonly OL WORLD_CENTER_OL = new(MIDDLE_ORDER, MIDDLE_LAYER);
		/// <summary>世界中心点，单位为Coord</summary>
		public static readonly Coord WORLD_CENTER_COORD = WORLD_CENTER_OL.ToCoord();
		/// <summary>世界中心点，单位为Unity世界坐标系的Vec3</summary>
		public static readonly Vector3 WORLD_CENTER_VEC3 = WORLD_CENTER_OL.ToVec3DefaultY();

		public const int MAX_SORTING_ORDER = 1000;
		public const int FRONT_SORTING_ORDER = 5;
		public const int VILL_SORTING_ORDER = 4;
		public const int ARCH_SORTING_ORDER = 3;
		public const int EARTH_SORTING_ORDER = 2;
		public const int BACK_SORTING_ORDER = 1;

		/// <summary>单位Coord的X，在Unity世界坐标系中X轴的长度</summary>
		public const float UX_PER_CX = 0.4f;
		/// <summary>单位Coord的Y，在Unity世界坐标系中Z轴的长度</summary>
		public const float UZ_PER_CY = 0.4f;
		/// <summary>地平线的高度，单位为Unity世界坐标系的Y轴</summary>
		public static float DEFAULT_Y = 0f;
		public static float LayerGap => CY_PER_LYR * UZ_PER_CY;
		/// <summary>1倍速下每秒的Tick数</summary>
		public const uint SPEEDx1_TICKS_PER_SECOND = 20;

		public const float DEFAULT_CAMERA_HEIGHT = 1.5f;

		/// <summary>  村庄边界的宽度，单位为OL。主要用于界定村民的随机移动范围。</summary>
		public const int DEFAULT_WORLD_EDGE_WIDTH = 2;

		[RuntimeInitializeOnLoadMethod]
		static void SetFixedDeltaTime() {
			Time.fixedDeltaTime = 1f / SPEEDx1_TICKS_PER_SECOND;
		}

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
		/* Coord Extension */
		public static SimpleVector3 ToVec3(this Coord coord, float y) {
			return new SimpleVector3(
				coord.X * ConstMgr.UX_PER_CX,
				y,
				coord.Y * ConstMgr.UZ_PER_CY);
		}
		public static SimpleVector3 ToVec3DefaultY(this Coord coord) {
			return new SimpleVector3(
				coord.X * ConstMgr.UX_PER_CX,
				ConstMgr.DEFAULT_Y,
				coord.Y * ConstMgr.UZ_PER_CY);
		}
		public static bool IsOL(this Coord coord) => coord.X % ConstMgr.CX_PER_ODR == 0 && coord.Y % ConstMgr.CY_PER_LYR == 0;
		public static bool IsOnLayer(this Coord coord) => coord.Y % ConstMgr.CY_PER_LYR == 0;
		public static OL ToOL(this Coord coord) {
			if (!coord.IsOL()) {
				throw new Exception($"Coord {coord} is not on a valid OL");
			}
			return new OL(coord.X / ConstMgr.CX_PER_ODR, coord.Y / ConstMgr.CY_PER_LYR);
		}
		public static List<OL> GetNeighborOLs(this Coord coord) {
			var rX = coord.X % ConstMgr.CX_PER_ODR;
			var rY = coord.Y % ConstMgr.CY_PER_LYR;
			if (rX == 0 && rY == 0) {
				var ol = new OL(coord.X / ConstMgr.CX_PER_ODR, coord.Y / ConstMgr.CY_PER_LYR);
				return ol.GetNeighbors();
			}
			if (rX == 0) {
				var ORD = coord.X / ConstMgr.CX_PER_ODR;
				var tmp = 1f * coord.Y / ConstMgr.CY_PER_LYR;
				// y 方向更大的
				var upper = new OL(ORD, Mathf.CeilToInt(tmp));
				// y 方向更小的
				var lower = new OL(ORD, Mathf.FloorToInt(tmp));
				if (upper.CheckAvailableForArch()) {
					return new() { upper, lower };
				} else {
					return new() { upper };
				}
			}
			if (rY == 0) {
				var LYR = coord.Y / ConstMgr.CY_PER_LYR;
				var tmp = 1f * coord.X / ConstMgr.CX_PER_ODR;
				return new() {
					new(Mathf.FloorToInt(tmp), LYR),
					new(Mathf.CeilToInt(tmp), LYR),
				};
			}
			throw new Exception($"Coord {coord} is not on a valid OL");
		}
		public static bool IsOnSameEdge(this Coord coord, Coord other) {
			if (coord.X == other.X && coord.X % ConstMgr.CX_PER_ODR == 0) {
				var dis = Mathf.Abs(coord.Y - other.Y);
				return dis < ConstMgr.CY_PER_LYR || (dis == ConstMgr.CY_PER_LYR && Mathf.Max(coord.Y, other.Y) / ConstMgr.CY_PER_LYR % 2 == 0);
			}
			if (coord.Y == other.Y && coord.Y % ConstMgr.CY_PER_LYR == 0) {
				return Mathf.Abs(coord.X - other.X) <= ConstMgr.CX_PER_ODR;
			}
			return false;
		}

		/* OL Extension */
		public static SimpleVector3 ToVec3(this OL ol, float y) {
			return new SimpleVector3(
				ConstMgr.UX_PER_CX * ConstMgr.CX_PER_ODR * ol.ODR,
				y,
				ConstMgr.UZ_PER_CY * ConstMgr.CY_PER_LYR * ol.LYR);
		}
		public static SimpleVector3 ToVec3DefaultY(this OL ol) {
			return new SimpleVector3(
				ConstMgr.UX_PER_CX * ConstMgr.CX_PER_ODR * ol.ODR,
				ConstMgr.DEFAULT_Y,
				ConstMgr.UZ_PER_CY * ConstMgr.CY_PER_LYR * ol.LYR);
		}
		public static Coord ToCoord(this OL ol) {
			return new Coord(ol.ODR * ConstMgr.CX_PER_ODR, ol.LYR * ConstMgr.CY_PER_LYR);
		}


	}
}