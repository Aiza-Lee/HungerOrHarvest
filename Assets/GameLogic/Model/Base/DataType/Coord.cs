using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;
using UnityEngine;

namespace GameLogic
{
	[Serializable]
	public struct Coord {
		public int X;
		public int Y;

		public Coord(int x, int y) {
			X = x;
			Y = y;
		}

		public void Translate(int x, int y) {
			X += x;
			Y += y;
		}
		public readonly bool IsOL() {
			return X % ConstMgr.X_PER_ODR == 0 && Y % ConstMgr.Y_PER_LYR == 0;
		}
		public readonly bool IsOnLayer() {
			return Y % ConstMgr.Y_PER_LYR == 0;
		}
		public readonly List<OL> GetNeighborOLs() {
			var rX = X % ConstMgr.X_PER_ODR;
			var rY = Y % ConstMgr.Y_PER_LYR;
			if (rX == 0 && rY == 0) {
				var ol = new OL(X / ConstMgr.X_PER_ODR, Y / ConstMgr.Y_PER_LYR);
				return ol.GetNeighbors();
			}
			if (rX == 0) {
				var ORD = X / ConstMgr.X_PER_ODR;
				var tmp = 1f * Y / ConstMgr.Y_PER_LYR;
				return new() {
					new(ORD, Mathf.FloorToInt(tmp)),
					new(ORD, Mathf.CeilToInt(tmp)),
				};
			}
			if (rY == 0) {
				var LYR = Y / ConstMgr.Y_PER_LYR;
				var tmp = 1f * X / ConstMgr.X_PER_ODR;
				return new() {
					new(Mathf.FloorToInt(tmp), LYR),
					new(Mathf.CeilToInt(tmp), LYR),
				};
			}
			throw new System.Exception();
		}
		public readonly int DistanceTo(Coord other) {
			return Mathf.Abs(X - other.X) + Mathf.Abs(Y - other.Y);
		}
		public readonly int Distance(OL ol) {
			return DistanceTo(ol.ToCoord());
		}
		public readonly bool OnSameEdge(Coord other) {
			if (X == other.X && X % ConstMgr.X_PER_ODR == 0) {
				var dis = Mathf.Abs(Y - other.Y);
				return dis < ConstMgr.Y_PER_LYR || (dis == ConstMgr.Y_PER_LYR && Mathf.Max(Y, other.Y) / ConstMgr.Y_PER_LYR % 2 == 0);
			}
			if (Y == other.Y && Y % ConstMgr.Y_PER_LYR == 0) {
				return Mathf.Abs(X - other.X) <= ConstMgr.X_PER_ODR;
			}
			return false;
		}
		public readonly Coord DirectionTo(Coord other) {
			var dX = other.X - X;
			var dY = other.Y - Y;
			if (dX != 0 && dY != 0) {
				throw new Exception("Not on same edge");
			}
			return new Coord(Mathf.Clamp(dX, -1, 1), Mathf.Clamp(dY, -1, 1));
		}



		public static bool operator==(Coord lhv, Coord rhv) {
			return lhv.X == rhv.X && lhv.Y == rhv.Y;
		}
		public static bool operator!=(Coord lhv, Coord rhv) {
			return lhv.X != rhv.X || lhv.Y != rhv.Y;
		}
		public static Coord operator+(Coord lhv, Coord rhv) {
			return new Coord(lhv.X + rhv.X, lhv.Y + rhv.Y);
		}
		public static Coord operator-(Coord lhv, Coord rhv) {
			return new Coord(lhv.X - rhv.X, lhv.Y - rhv.Y);
		}
		public override readonly bool Equals(object obj) {
			if (obj is Coord ol) {
				return ol.X == X && ol.Y == Y;
			} else {
				return false;
			}
		}
		public override readonly int GetHashCode() {
			return HashCode.Combine(X, Y);
		}
		public override readonly string ToString() {
			return $"({X}, {Y})";
		}

	}
}