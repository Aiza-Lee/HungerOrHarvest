using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public class SimpleNodeViewDataSO : NSNodeViewDataSO {
		public string OutputPortID;

		public override void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			if (nodeView is SimpleNodeView simpleNodeView) {
				ports.Add(OutputPortID, simpleNodeView.OutputPort);
			} else {
				Debug.LogError("NS: Cannot get output port.");
			}
		}
		public override void SetNodeView(NSNodeViewBase nodeViewBase) {
		}
	}
}