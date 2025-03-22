using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NSFrame
{
	using static GraphViewUtility;
	public abstract class NSNodeViewBase : Node {
		const string DEFAULT_NODE_NAME = "Node View";
		public string ID { get; set; }
		public string NodeViewName { get; set; }
		public Type NodeViewType { get; set; }
		public NSGroupView GroupView { get; set; }
		public NSPort InputPort { get; set; }
		public TextField NodeNameTF { get; set; }
		protected NSGraphView _graphView;

		public void Generate(NSGraphView graphView, Type nodeviewType, Vector2 position, string nodeViewName) {
			ID = Guid.NewGuid().ToString();
			if (string.IsNullOrEmpty(nodeViewName)) {
				nodeViewName = DEFAULT_NODE_NAME;
			}
			NodeViewName = nodeViewName;
			NodeViewType = nodeviewType;
			SetPosition(new Rect(position, Vector2.zero));
			_graphView = graphView;

			mainContainer.AddClasses("nsframe-nodeview__main-container");
			extensionContainer.AddClasses("nsframe-nodeview__extension-container");
		}
		public void Draw() {
			/* TITLE CONTAINER */
			NodeNameTF = CreateTextField(NodeViewName, null, evt => {
				TextField target = evt.target as TextField;
				if (string.IsNullOrEmpty(target.value)) {
					if (!string.IsNullOrEmpty(NodeViewName)) ++_graphView.NameErrorCount;
				}
				else {
					if (string.IsNullOrEmpty(NodeViewName)) --_graphView.NameErrorCount;
				}

				if (GroupView == null) {
					_graphView.RemoveUngroupedNodeView(this);
					NodeViewName = evt.newValue;
					_graphView.AddUngroupedNodeView(this);
				}
				else {
					NSGroupView curGroup = GroupView;
					_graphView.RemoveGroupedNodeView(this, GroupView.title);
					NodeViewName = evt.newValue;
					_graphView.AddGroupedNodeView(this, curGroup.title);
				}
			});
			NodeNameTF.AddClasses(
				"nsframe-nodeview__filename-textfield",
				"nsframe-nodeview__textfield",
				"nsframe-nodeview__textfield__hidden"
			);
			titleContainer.Insert(0, NodeNameTF);

			/* INPUT CONTAINER */
			InputPort = this.CreatePort("in", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
			inputContainer.Add(InputPort);

			/* OUTPUT CONTAINER */
			DesignOutputContainer();

			/* EXTENSION CONTAINER */
			DesignExtensionContainer();

			// 扩展部分不会自动刷新
			RefreshExpandedState();
		}
		/// <summary>
		/// 覆写须知：请调用 outputContainer
		/// </summary>
		public abstract void DesignOutputContainer();
		/// <summary>
		/// 覆写须知：请调用 extensionContainer
		/// </summary>
		public abstract void DesignExtensionContainer();
		
	  #region Save Methods
	  	public abstract NSNodeViewDataSO GetEmptyNodeViewDataSO();
		protected abstract void SetAdditionalViewDataSO(NSNodeViewDataSO nodeViewDataSO);
	  	public void SetViewDataSO(NSNodeViewDataSO dataSO) {
			dataSO.NodeViewName = NodeViewName;
			dataSO.InputPortID = InputPort.ID;
			dataSO.Position = GetPosition().position;
			if (GroupView == null) {
				dataSO.name = $"Node__{NodeViewName}";
			} else {
				dataSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}

			SetAdditionalViewDataSO(dataSO);
		}
	  #endregion
	  #region Export Methods
	  	public abstract NSNodeSOBase GetEmptyNodeSO();
		protected abstract void SetAdditionalNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs);
		public void SetNodeSO(NSNodeSOBase nodeSO, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {
			nodeSO.NodeName = NodeViewName;
			
			// TODO: 从整个图中寻找会有性能浪费
			nodeSO.PreviousNodes = new();
			foreach (var edge in graphView.edges) {
				if (edge.input.node == this) {
					nodeSO.PreviousNodes.Add(nodeSOs[(edge.output.node as NSNodeViewBase).ID]);
				}
			}
			if (GroupView == null) {
				nodeSO.name = $"Node__{NodeViewName}";
			} else {
				nodeSO.name = $"NodeInGroup__{GroupView.title}__{NodeViewName}";
			}

			SetAdditionalNodeSO(nodeSO, graphView, nodeSOs);
		}
	  #endregion

	  #region Utility Methods
	  	public void DisconnectAllPorts() {
			DisconnectPorts(inputContainer);
			DisconnectPorts(outputContainer);
		}
		private void DisconnectPorts(VisualElement container) {
			foreach (VisualElement element in container.Children()) 
				if (element is Port port && port.connected) 
					_graphView.DeleteElements(port.connections);
		}
		public void SetErrorStyle(Color color) {
			mainContainer.style.backgroundColor = color;
		}
		public void RemoveErrorStyle() {
			mainContainer.style.backgroundColor = StyleKeyword.Null;
		}
	  #endregion
	}
}