using System;
using System.Collections.Generic;

namespace GameLogic.Common.DataTypes {
	public static class EtListExtension {
		public static EtList<E, T> ToEtList<E, T>(this IEnumerable<EtPair<E, T>> pairs)
		where E : Enum
		where T : struct {
			return new EtList<E, T>(pairs);
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
		where T : struct, IComparable<T> {
			foreach (var pr in other) {
				if (self_F[pr.Index].CompareTo(pr.Value) <= 0) {
					return false;
				}
			}
			return true;
		}
	}
	
	// 为 float 类型特化的扩展方法
	public static class EtListFloatExtension {
		public static EtList<E, float> Sub<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] - pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, float> Sub_New<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			return new EtList<E, float>(self_F).Sub(others);
		}

		public static EtList<E, float> Add<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] + pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, float> Add_New<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			return new EtList<E, float>(self_F).Add(others);
		}

		public static EtList<E, float> Mul<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] * pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, float> Mul_New<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			return new EtList<E, float>(self_F).Mul(others);
		}

		public static EtList<E, float> Div<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] / pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, float> Div_New<E>(this EtList<E, float> self_F, params EtList<E, float>[] others)
		where E : Enum {
			return new EtList<E, float>(self_F).Div(others);
		}
	}
	
	// 为 int 类型特化的扩展方法
	public static class EtListIntExtension {
		public static EtList<E, int> Sub<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] - pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, int> Sub_New<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			return new EtList<E, int>(self_F).Sub(others);
		}

		public static EtList<E, int> Add<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] + pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, int> Add_New<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			return new EtList<E, int>(self_F).Add(others);
		}

		public static EtList<E, int> Mul<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] * pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, int> Mul_New<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			return new EtList<E, int>(self_F).Mul(others);
		}

		public static EtList<E, int> Div<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			foreach (var other in others) {
				other.ForEach(pr => {
					self_F[pr.Index] = self_F[pr.Index] / pr.Value;
				});
			}
			return self_F;
		}
		public static EtList<E, int> Div_New<E>(this EtList<E, int> self_F, params EtList<E, int>[] others)
		where E : Enum {
			return new EtList<E, int>(self_F).Div(others);
		}
	}
}