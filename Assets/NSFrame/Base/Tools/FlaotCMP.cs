using System;
using UnityEngine;

namespace NSFrame {
	public static class FlaotUtilities {
		const float EPS = 1e-2f;
		public static bool IsApproximatelyEqual(this float a, float b, float eps = EPS) {
			return Math.Abs(a - b) < eps;
		}
		public static bool IsApproximatelyEqual(this Vector3 a, Vector3 b, float eps = EPS) {
			return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z) < eps;
		}

		public static float Clamp01(this float a) {
			return Mathf.Clamp01(a);
		}
	}
}