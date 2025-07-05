using System;
using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	/// <summary>
	/// 配置平滑变化的曲线类型和总时间
	/// <para>用于在渲染组件中配置平滑变化的动画效果</para>
	/// </summary>
	[Serializable]
	public class SmoothChangeConfigComp : IComponent {

		public ChangeCurveType ChangeCurveType;
		public float TotalTime;

		public void CopyFrom(IComponent other) {
			if (other is SmoothChangeConfigComp otherComp) {
				ChangeCurveType = otherComp.ChangeCurveType;
				TotalTime = otherComp.TotalTime;
			} else {
				throw new ArgumentException("Cannot copy from non-SmoothChangeConfigComp component");
			}
		}
	}
}