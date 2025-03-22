using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public abstract class NSNodeViewDataSO : ScriptableObject {
		public string NodeViewName;
		public string NodeViewType;
		public Vector2 Position;
		public string InputPortID;
		/// <summary>
		/// 覆写须知：在 参数ports 中存储 参数nodeView 中所有的OutputPort信息
		/// </summary>
		/// <param name="nodeView"> 需要处理的nodeViewBase对象 </param>
		/// <param name="ports"></param>
		public abstract void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports);
		/// <summary>
		/// 覆写须知：完善派生nodeView的存储信息
		/// </summary>
		/// <param name="nodeViewBase"> 需要完善的NodeViewBase对象 </param>
		public abstract void SetNodeView(NSNodeViewBase nodeViewBase);
		public void GetInputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			ports.Add(InputPortID, nodeView.InputPort);
		}
	}
}