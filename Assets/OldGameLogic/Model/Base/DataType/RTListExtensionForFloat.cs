using System;
using UnityEngine;

namespace OldGameLogic
{
	public static class RTListExtension {
		public static RTList<float> Sub_New(this RTList<float> self_F, params RTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Sub when list is not full.");
				return self_F;
			}
			return self_F.Clone().Sub(others);
		}
		public static RTList<float> Sub(this RTList<float> self_F, params RTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Sub when list is not full.");
				return self_F;
			}
			foreach (var other in others) {
				foreach (var item in  other.List) {
					self_F[item.Index].Value -= item.Value;
				}
			}
			return self_F;
		}
		public static RTList<float> Add_New(this RTList<float> self_F, params RTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Add when list is not full.");
				return self_F;
			}
			return self_F.Clone().Add(others);
		}
		public static RTList<float> Add(this RTList<float> self_F, params RTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Add when list is not full.");
				return self_F;
			}
			foreach (var other in others) {
				foreach (var item in other.List) {
					self_F[item.Index].Value += item.Value;
				}
			}
			return self_F;
		}
		public static RTList<float> Change_New(this RTList<float> self, Func<float, float> func) {
			return self.Clone().Change(func);
		}
		public static RTList<float> Change(this RTList<float> self, Func<float, float> func) {
			self.List.ForEach(item => {
				item.Value = func(item.Value);
			});
			return self;
		}
		/// <summary>
		/// 对自己的每一位乘上参数中对应位的值，返回新的对象
		/// </summary>
		/// <param name="other_F"> 乘上的值 </param>
		public static RTList<float> Mul_New(this RTList<float> self, RTList<float> other_F) {
			if (!other_F.Full) {
				Debug.LogWarning("Donnot use Mul when list is not full.");
				return self;
			}
			return self.Clone().Mul(other_F);
		}
		/// <summary>
		/// 对自己的每一位乘上参数中对应位的值，修改在原对象上进行
		/// </summary>
		/// <param name="other_F"> 乘上的值 </param>
		public static RTList<float> Mul(this RTList<float> self, RTList<float> other_F) {
			if (!other_F.Full) {
				Debug.LogWarning("Donnot use Mul when list is not full.");
				return self;
			}
			foreach (var item in self.List) {
				item.Value *= other_F[item.Index].Value;
			}
			return self;
		}


		public static bool BigEnoughThan(this RTList<float> self_F, RTList<float> other) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use BiggerThan when list is not full.");
				return false;
			}
			foreach (var item in other.List) {
				if (self_F[item.Index].Value < item.Value) {
					return false;
				}
			}
			return true;
		}
	}
}