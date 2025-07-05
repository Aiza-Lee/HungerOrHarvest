using System;
using System.Collections.Generic;
using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.Logic {
	/// <summary>
	/// 逻辑中用于定位建筑和道路的大坐标，可以按照ConstMgr中的配置映射到Coord坐标
	/// <para>方向:面向地图从上往下看，ODR正方向为从左到右（→），LYR正方向为从下到上（↑）</para>
	/// <para>规定(ODR+LYR)为偶数的坐标可以放置建筑</para>
	/// </summary>
	public class OL : IEquatable<OL>, IComponent {
		public int ODR;
		public int LYR;
		public OL(int odr, int lyr) {
			ODR = odr;
			LYR = lyr;
		}

		#region PublicMethods
		public OL Translate(int odr, int lyr) {
			return new(ODR + odr, LYR + lyr);
		}
		public Coord ToCoord() {
			return new(ODR * ConstMgr.X_PER_ODR, LYR * ConstMgr.Y_PER_LYR);
		}
		public List<OL> GetNeighbors() {
			return new() {
				new(ODR - 1, LYR),
				new(ODR + 1, LYR),
				new(ODR, LYR + (Mathf.Abs(LYR + ODR) % 2 == 0 ? -1 : 1))
			};
		}
		public int Distance(OL other) {
			return Mathf.Abs(ODR - other.ODR) + Mathf.Abs(LYR - other.LYR);
		}
		public bool CheckAvailableForArch() {
			return Mathf.Abs(ODR + LYR) % 2 == 0;
		}
		#endregion

		public static bool operator ==(OL lhv, OL rhv) => lhv.ODR == rhv.ODR && lhv.LYR == rhv.LYR;
		public static bool operator !=(OL lhv, OL rhv) => lhv.ODR != rhv.ODR || lhv.LYR != rhv.LYR;
		public override bool Equals(object obj) {
			if (obj is OL ol) {
				return ol.ODR == ODR && ol.LYR == LYR;
			} else {
				return false;
			}
		}
		public override int GetHashCode() => HashCode.Combine(ODR, LYR);
		public override string ToString() => $"[{ODR}, {LYR}]";
		public bool Equals(OL other) => ODR == other.ODR && LYR == other.LYR;
	}
}