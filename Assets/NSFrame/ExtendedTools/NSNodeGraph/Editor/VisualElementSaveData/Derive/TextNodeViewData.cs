using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public class TextNodeViewDataSO : NSNodeViewDataSO {
		public string OutputPortID;
		public string Text;

		public override void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			if (nodeView is TextNodeView textNodeView) {
				ports.Add(OutputPortID, textNodeView.OutputPort);
			} else {
				Debug.LogError("NS: Cannot get output port.");
			}
		}

		public override void SetNodeView(NSNodeViewBase nodeViewBase) {
			var nodeView = nodeViewBase as TextNodeView;
			nodeView.TextTF.value = Text;
		}
	}
}