using System;
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
		
		public readonly int DistanceTo(Coord other) {
			return Mathf.Abs(X - other.X) + Mathf.Abs(Y - other.Y);
		}
		public readonly int Distance(OL ol) {
			return DistanceTo(ol.ToCoord());
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