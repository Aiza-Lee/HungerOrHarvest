using System.Collections.Generic;

namespace NSFrame 
{
	public class NodeViewErrorData {
		public ErrorColor ErrorColor { get; set; }
		public List<NSNodeViewBase> NodeViews { get; set; }

		public NodeViewErrorData() {
			ErrorColor = new();
			NodeViews = new();
		}
	}
}