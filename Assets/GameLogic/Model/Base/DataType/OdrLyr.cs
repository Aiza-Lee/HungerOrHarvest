using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public struct OL {
		public int ODR;
		public int LYR;
		public OL(int odr, int lyr) {
			ODR = odr;
			LYR = lyr;
		}
		public readonly OL Translate(int odr, int lyr) {
			return new(ODR + odr, LYR + lyr);
		}
		public readonly Coord ToCoord() {
			return new(ODR * ConstMgr.X_PER_ODR, LYR * ConstMgr.Y_PER_LYR);
		}
		public readonly List<OL> GetNeighbors() {
			return new() {
				new(ODR - 1, LYR),
				new(ODR + 1, LYR),
				new(ODR, LYR + ((LYR + ODR) % 2 == 0 ? -1 : 1))
			};
		}
		public readonly int Distance(OL other) {
			return Mathf.Abs(ODR - other.ODR) + Mathf.Abs(LYR - other.LYR);
		}
		


		public static bool operator==(OL lhv, OL rhv) {
			return lhv.ODR == rhv.ODR && lhv.LYR == rhv.LYR;
		}
		public static bool operator!=(OL lhv, OL rhv) {
			return lhv.ODR != rhv.ODR || lhv.LYR != rhv.LYR;
		}
		public override readonly bool Equals(object obj) {
			if (obj is OL ol) {
				return ol.ODR == ODR && ol.LYR == LYR;
			} else {
				return false;
			}
		}
		public override readonly int GetHashCode() {
			return HashCode.Combine(ODR, LYR);
		}
		public override readonly string ToString() {
			return $"([{ODR}, {LYR}])";
		}
	}
}