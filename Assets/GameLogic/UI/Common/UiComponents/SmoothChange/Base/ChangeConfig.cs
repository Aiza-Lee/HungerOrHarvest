using System;
using GameLogic.Common.View;

namespace GameLogic.UI.Common.UiComponents.SmoothChange {
	[System.Serializable]
	public class ChangeConfig {
		public ChangeCurveType CurveType;
		public float Time;

		public Func<float, float> Curve => ChangeCurves.GetCurve(CurveType);

		/// <summary>
		/// 设置为一条y=x从0到1的线段
		/// </summary>
		public ChangeConfig SetLinearCurve(float time) {
			CurveType = ChangeCurveType.Linear;
			Time = time;
			return this;
		}
	}
}