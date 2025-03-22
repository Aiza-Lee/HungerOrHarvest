using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NSFrame
{
	public class SimpleNodeView : NSNodeViewBase {
		public NSPort OutputPort { get; set; }

		public override void DesignExtensionContainer() {
			return;
		}
		public override void DesignOutputContainer() {
			OutputPort = this.CreatePort("out", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
			outputContainer.Add(OutputPort);
		}

		public override NSNodeViewDataSO GetEmptyNodeViewDataSO() {
			return IOUtility.CreateSO<SimpleNodeViewDataSO>();
		}
		protected override void SetAdditionalViewDataSO(NSNodeViewDataSO nodeViewDataSO) {
			if (nodeViewDataSO is not SimpleNodeViewDataSO) {
				Debug.LogError("NS: NodeViewDataSO type wrong.");
				return;
			}
			var viewDataSO = nodeViewDataSO as SimpleNodeViewDataSO;

			viewDataSO.NodeViewType = NodeViewType.FullName;
			viewDataSO.OutputPortID = OutputPort.ID;
		}

		public override NSNodeSOBase GetEmptyNodeSO() {
			return IOUtility.CreateSO<SimpleNodeSO>();
		}
		protected override void SetAdditionalNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {
			if (nodeSOBase is not SimpleNodeSO) {
				Debug.LogError("NS: NodeSO type wrong.");
				return;
			}
			var nodeSO = nodeSOBase as SimpleNodeSO;

			nodeSO.NodeSOType = NodeViewType.FullName;
			nodeSO.NextNodes = new();
			foreach (var edge in graphView.edges) {
				if (edge.output.node == this) {
					nodeSO.NextNodes.Add(nodeSOs[(edge.input.node as NSNodeViewBase).ID]);
				}
			}
		}
	}
}