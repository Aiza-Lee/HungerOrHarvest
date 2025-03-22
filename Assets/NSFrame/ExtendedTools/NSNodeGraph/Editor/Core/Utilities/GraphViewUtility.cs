using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.Port;

namespace NSFrame
{
	public static class GraphViewUtility {

		public static Button CreateButton(string text, Action onClick = null) {
			Button button = new(onClick) {
				text = text
			};
			return button;
		}

		public static Foldout CreateFoldOut(string title, bool collapsed = false) {
			Foldout foldout = new() {
				text = title,
				value = !collapsed
			};
			return foldout;
		}

		public static NSPort CreatePort(this NSNodeViewBase nodeView, string portName, 
		Orientation orientation, Direction direction, Port.Capacity capacity, Type valueType = null) 
		{
			// NSPort port = nodeView.InstantiatePort(orientation, direction, capacity, valueType) as NSPort;
			NSPort port = new(orientation, direction, capacity, valueType) {
				portName = portName,
				ID = Guid.NewGuid().ToString()
			};
			port.AddManipulator(new EdgeConnector<Edge>(new DefaultEdgeConnectorListener()));
			return port;
		}
		/// <summary>
		/// 从反汇编代码中粘贴过来的
		/// </summary>
		private class DefaultEdgeConnectorListener : IEdgeConnectorListener {
			private GraphViewChange m_GraphViewChange;
			private List<Edge> m_EdgesToCreate;
			private List<GraphElement> m_EdgesToDelete;

			public DefaultEdgeConnectorListener() {
				m_EdgesToCreate = new List<Edge>();
				m_EdgesToDelete = new List<GraphElement>();
				m_GraphViewChange.edgesToCreate = m_EdgesToCreate;
			}
			public void OnDropOutsidePort(Edge edge, Vector2 position) {}
			public void OnDrop(GraphView graphView, Edge edge) {
				m_EdgesToCreate.Clear();
				m_EdgesToCreate.Add(edge);
				m_EdgesToDelete.Clear();
				if (edge.input.capacity == Capacity.Single) {
					foreach (Edge connection in edge.input.connections) {
						if (connection != edge) {
							m_EdgesToDelete.Add(connection);
						}
					}
				}
				if (edge.output.capacity == Capacity.Single) {
					foreach (Edge connection2 in edge.output.connections) {
						if (connection2 != edge) {
							m_EdgesToDelete.Add(connection2);
						}
					}
				}
				if (m_EdgesToDelete.Count > 0) {
					graphView.DeleteElements(m_EdgesToDelete);
				}

				List<Edge> edgesToCreate = m_EdgesToCreate;
				if (graphView.graphViewChanged != null) {
					edgesToCreate = graphView.graphViewChanged(m_GraphViewChange).edgesToCreate;
				}
				foreach (Edge item in edgesToCreate) {
					graphView.AddElement(item);
					edge.input.Connect(item);
					edge.output.Connect(item);
				}
			}
		}
		
		/// <summary>
		/// 便捷创建 TextField 的方法
		/// </summary>
		/// <param name="onValueChanged">内容改变 callBack </param>
		public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null) {
			TextField textField = new() { value = value, label = label };
			if (onValueChanged != null) 
				textField.RegisterValueChangedCallback(onValueChanged);
			return textField;
		}
    }
}
