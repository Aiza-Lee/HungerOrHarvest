using System;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	/// <summary>
	/// 应用了平滑变化的坐标组件。
	/// 该组件用于存储坐标信息以及平滑变化的相关参数。
	/// </summary>
	[Serializable]
	public class SmoothedCoordComponent : IComponent {
		public Coord Coord;
		public ChangeCurveType ChangeCurveType;
		public float TotalTime = 0f;
		public bool IsDirty = true;
	}
}