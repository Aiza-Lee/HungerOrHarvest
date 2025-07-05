using UnityEngine;

namespace GameLogic.Common.ViewsMono.SmoothChange {
	[System.Serializable]
	public class ChangeConfig {
		public AnimationCurve Curve;
		public float Time;
		/// <summary>
		/// 设置为一条y=x从0到1的线段
		/// </summary>
		public ChangeConfig SetLinearCurve(float time) {
			Curve = AnimationCurve.Linear(0, 0, 1, 1);
			Time = time;
			return this;
		}
	}
}