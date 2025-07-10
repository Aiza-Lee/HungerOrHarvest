using System;
using System.Collections.Generic;
using GameLogic.Common.Logic;
using UnityEngine;

namespace GameLogic.Common.DataTypes {
	/// <summary>
	/// 逻辑世界最小坐标
	/// <para>方向:面向地图从上往下看，X正方向为从左到右（→），Y正方向为从下到上（↑）</para>
	/// </summary>
	[Serializable]
	public struct Coord : IEquatable<Coord> {
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
		public readonly bool IsOL() => X % ConstMgr.CX_PER_ODR == 0 && Y % ConstMgr.CY_PER_LYR == 0;
		public readonly bool IsOnLayer() => Y % ConstMgr.CY_PER_LYR == 0;
		
		public readonly List<OL> GetNeighborOLs() {
			var rX = X % ConstMgr.CX_PER_ODR;
			var rY = Y % ConstMgr.CY_PER_LYR;
			if (rX == 0 && rY == 0) {
				var ol = new OL(X / ConstMgr.CX_PER_ODR, Y / ConstMgr.CY_PER_LYR);
				return ol.GetNeighbors();
			}
			if (rX == 0) {
				var ORD = X / ConstMgr.CX_PER_ODR;
				var tmp = 1f * Y / ConstMgr.CY_PER_LYR;
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
				var LYR = Y / ConstMgr.CY_PER_LYR;
				var tmp = 1f * X / ConstMgr.CX_PER_ODR;
				return new() {
					new(Mathf.FloorToInt(tmp), LYR),
					new(Mathf.CeilToInt(tmp), LYR),
				};
			}
			throw new Exception($"Coord {this} is not on a valid OL");
		}
		public readonly int DistanceTo(Coord other) {
			return Mathf.Abs(X - other.X) + Mathf.Abs(Y - other.Y);
		}
		public readonly int Distance(OL ol) {
			return DistanceTo(ol.ToCoord());
		}
		public readonly bool OnSameEdge(Coord other) {
			if (X == other.X && X % ConstMgr.CX_PER_ODR == 0) {
				var dis = Mathf.Abs(Y - other.Y);
				return dis < ConstMgr.CY_PER_LYR || (dis == ConstMgr.CY_PER_LYR && Mathf.Max(Y, other.Y) / ConstMgr.CY_PER_LYR % 2 == 0);
			}
			if (Y == other.Y && Y % ConstMgr.CY_PER_LYR == 0) {
				return Mathf.Abs(X - other.X) <= ConstMgr.CX_PER_ODR;
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



		public static bool operator ==(Coord lhv, Coord rhv) => lhv.X == rhv.X && lhv.Y == rhv.Y;
		public static bool operator !=(Coord lhv, Coord rhv) => lhv.X != rhv.X || lhv.Y != rhv.Y;
		public static Coord operator +(Coord lhv, Coord rhv) => new(lhv.X + rhv.X, lhv.Y + rhv.Y);
		public static Coord operator -(Coord lhv, Coord rhv) => new(lhv.X - rhv.X, lhv.Y - rhv.Y);
		public override readonly bool Equals(object obj) {
			if (obj is Coord ol) {
				return ol.X == X && ol.Y == Y;
			} else {
				return false;
			}
		}
		public override readonly int GetHashCode() => HashCode.Combine(X, Y);
		public override readonly string ToString() => $"({X}, {Y})";
		public readonly bool Equals(Coord other) => X == other.X && Y == other.Y;
	}
}