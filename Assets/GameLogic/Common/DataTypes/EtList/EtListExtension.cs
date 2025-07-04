using System;
using System.Collections.Generic;

namespace GameLogic.Common.DataTypes {
	public static class EtListExtension {
		public static EtList<E, T> ToEtList<E, T>(this IEnumerable<EtPair<E, T>> pairs)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(pairs);
		}

		public static EtList<E, T> Sub<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = (dynamic) self_F[pr.Index] - (dynamic) pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, T> Sub_New<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(self_F).Sub(others);
		}

		public static EtList<E, T> Add<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = (dynamic) self_F[pr.Index] + (dynamic) pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, T> Add_New<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(self_F).Add(others);
		}

		public static EtList<E, T> Mul<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = (dynamic) self_F[pr.Index] * (dynamic) pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, T> Mul_New<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(self_F).Mul(others);
		}

		public static EtList<E, T> Div<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = (dynamic) self_F[pr.Index] / (dynamic) pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, T> Div_New<E, T>(this EtList<E, T> self_F, params EtList<E, T>[] others)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(self_F).Div(others);
		}

		public static EtList<E, T> Change<E, T>(this EtList<E, T> self, Func<T, T> func)
		where E : Enum
		where T : struct {
			self.ForEach(pair => {
				pair.Value = func(pair.Value);
			});
			return self;
		}
		public static EtList<E, T> Change_New<E, T>(this EtList<E, T> self, Func<T, T> func)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(self).Change(func);
		}

		public static bool BiggerThan<E, T>(this EtList<E, T> self_F, EtList<E, T> other)
		where E : Enum
		where T : struct {
			foreach (var pr in other) {
				if (((IComparable)self_F[pr.Index]).CompareTo(pr.Value) <= 0) {
					return false;
				}
			}
			return true;
		}
	}
}