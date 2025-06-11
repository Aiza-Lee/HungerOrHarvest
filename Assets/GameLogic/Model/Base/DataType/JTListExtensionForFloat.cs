using System;
using UnityEngine;

namespace GameLogic {
	public static class JTListExtension {
		public static JTList<float> Sub_New(this JTList<float> self_F, params JTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Sub when list is not full.");
				return self_F;
			}
			return self_F.Clone().Sub(others);
		}
		public static JTList<float> Sub(this JTList<float> self_F, params JTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Sub when list is not full.");
				return self_F;
			}
			foreach (var other in others) {
				foreach (var item in other.List) {
					self_F[item.Index].Value -= item.Value;
				}
			}
			return self_F;
		}
		public static JTList<float> Add_New(this JTList<float> self_F, params JTList<float>[] others) {
			if (!self_F.Full) {
				Debug.LogWarning("Donnot use Add when list is not full.");
				return self_F;
			}
			return self_F.Clone().Add(others);
		}
		public static JTList<float> Add(this JTList<float> self_F, params JTList<float>[] others) {
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
		public static JTList<float> Change_New(this JTList<float> self, Func<float, float> func) {
			return self.Clone().Change(func);
		}
		public static JTList<float> Change(this JTList<float> self, Func<float, float> func) {
			self.List.ForEach(item => {
				item.Value = func(item.Value);
			});
			return self;
		}
		public static JTList<float> Mul_New(this JTList<float> self, JTList<float> other_F) {
			if (!other_F.Full) {
				Debug.LogWarning("Donnot use Mul when list is not full.");
				return self;
			}
			return self.Clone().Mul(other_F);
		}
		public static JTList<float> Mul(this JTList<float> self, JTList<float> other_F) {
			if (!other_F.Full) {
				Debug.LogWarning("Donnot use Mul when list is not full.");
				return self;
			}
			foreach (var item in self.List) {
				item.Value *= other_F[item.Index].Value;
			}
			return self;
		}
		public static bool BigEnoughThan(this JTList<float> self_F, JTList<float> other) {
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
