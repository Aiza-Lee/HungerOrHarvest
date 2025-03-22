using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NSFrame
{
	public class NSGraphView : GraphView {
		/// <summary>
		/// nodeView名字 -> errorData（包含同名的 nodeView 的引用 list 以及错误颜色）
		/// </summary>
		private Dictionary<string, NodeViewErrorData> _ungroupedNodeViews;
		/// <summary>
		/// groupViewのTitle -> (nodeView名字 -> errorData)
		/// </summary>
		private Dictionary<string, Dictionary<string, NodeViewErrorData>> _groupedNodeViews;
		private NSGraphEditor _graphEditor;

		private MiniMap _miniMap;
		private const float DEFAULT_MINIMAP_HEIGHT = 160;
		private const float DEFAULT_MINIMAP_WIDTH = 200;

		private int _nameErrorCount = 0;
		public int NameErrorCount { 
			get => _nameErrorCount; 
			set {
				_nameErrorCount = value;
				if (_nameErrorCount == 0) _graphEditor.EnableSaving();
				else if (_nameErrorCount == 1) _graphEditor.DisableSaving();
			} 
		}
		public List<Type> DerivedNodeViewTypes { get; set; }

		public NSGraphView(NSGraphEditor graphEditor) {
			_graphEditor = graphEditor;
			_ungroupedNodeViews = new();
			_groupedNodeViews = new();
			InitDerivedNodeViewTypes();

			AddComponents();

			OverrideCallbacks();
		}

	#region Editor Tools

	  #region Save Methods
		public NSGraphViewDataSO CreateViewDataSO(string folderPath, string fileName) {
			var graphViewDataSO = IOUtility.CreateSO<NSGraphViewDataSO>(folderPath, fileName);
			SetGraphViewDataSO(graphViewDataSO);
			IOUtility.SaveAssets(graphViewDataSO);
			return graphViewDataSO;
		}
	  	private void SetGraphViewDataSO(NSGraphViewDataSO graphViewDataSO) {

			graphViewDataSO.GroupViewDatas = new();
			graphViewDataSO.UngroupedNodeViewDatas = new();
			graphViewDataSO.Edges = new();

			foreach (var element in graphElements) {
				if (element is NSNodeViewBase nodeView && nodeView.GroupView == null) {
					var nodeViewDataSO = nodeView.GetEmptyNodeViewDataSO();
					nodeView.SetViewDataSO(nodeViewDataSO);

					graphViewDataSO.UngroupedNodeViewDatas.Add(nodeViewDataSO);
					AssetDatabase.AddObjectToAsset(nodeViewDataSO, graphViewDataSO);
				}
				else if (element is NSGroupView groupView) {
					var groupViewDataSO = groupView.GetViewDataSO();

					graphViewDataSO.GroupViewDatas.Add(groupViewDataSO);
					AssetDatabase.AddObjectToAsset(groupViewDataSO, graphViewDataSO);

					/* nodes in group */
					foreach (NSNodeViewDataSO nodeViewDataSO in groupViewDataSO.InGroupNodeViewDatas) {
						AssetDatabase.AddObjectToAsset(nodeViewDataSO, graphViewDataSO);
					}
				}
				else if (element is Edge edge) {
					graphViewDataSO.Edges.Add(new((edge.input as NSPort).ID, (edge.output as NSPort).ID));
				}
			}

		}
	  #endregion

	  #region Import Methods
		public void Import(NSGraphViewDataSO graphViewDataSO) {
			ClearGraphView();
			// PortID -> Port
			Dictionary<string, NSPort> ports = new();
			foreach (var nodeViewData in graphViewDataSO.UngroupedNodeViewDatas) {
				ImportNodeViewData(nodeViewData, ports);
			}
			foreach (var groupViewData in graphViewDataSO.GroupViewDatas) {
				ImportGroupViewData(groupViewData, ports);
			}
			foreach (var edge in graphViewDataSO.Edges) {
				AddElement(ports[edge.Key].ConnectTo(ports[edge.Value]));
			}
		}
		private NSNodeViewBase ImportNodeViewData(NSNodeViewDataSO nodeViewData, Dictionary<string, NSPort> ports) {
			Type type = GetType(nodeViewData.NodeViewType);
			NSNodeViewBase nodeView = CreateNodeView(type, nodeViewData.Position, nodeViewData.NodeViewName);
			nodeViewData.GetInputPort(nodeView, ports);
			nodeViewData.GetOutputPort(nodeView, ports);
			nodeViewData.SetNodeView(nodeView);
			return nodeView;
		}
		private Type GetType(string typeName) {
			foreach (var type in DerivedNodeViewTypes) {
				if (typeName == type.FullName) {
					return type;
				}
			}
			return null;
		}
		private NSGroupView ImportGroupViewData(NSGroupViewDataSO groupViewData, Dictionary<string, NSPort> ports) {
			NSGroupView groupView = CreateGroupView(groupViewData.Position, groupViewData.Title);
			foreach (var nodeViewData in groupViewData.InGroupNodeViewDatas) {
				NSNodeViewBase nodeView = ImportNodeViewData(nodeViewData, ports);
				groupView.AddElement(nodeView);
			}
			return groupView;
		}
	  #endregion

	  #region Export Methods
	  	public NSGraphSO CreateGraphSO(string folderPath, string fileName) {
			var graphSO = IOUtility.CreateSO<NSGraphSO>(folderPath, fileName);
			SetGraphSO(graphSO);
			IOUtility.SaveAssets(graphSO);
			return graphSO;
		}
		private void SetGraphSO(NSGraphSO graphSO) {
			graphSO.Groups = new();
			graphSO.UngroupedNodes = new();

			// nodeViewID -> nodeSO
			Dictionary<string, NSNodeSOBase> idToNodeSOs = new();
			// nodeView -> nodeSO
			Dictionary<NSNodeViewBase, NSNodeSOBase> viewToSOs = new();

			foreach (var element in graphElements) {
				if (element is NSNodeViewBase nodeView1 && nodeView1.GroupView == null) {
					var nodeSO = nodeView1.GetEmptyNodeSO();

					idToNodeSOs.Add(nodeView1.ID, nodeSO);
					viewToSOs.Add(nodeView1, nodeSO);
					graphSO.UngroupedNodes.Add(nodeSO);
					nodeSO.BelongedGroup = null;
					AssetDatabase.AddObjectToAsset(nodeSO, graphSO);
				}
				else if (element is NSGroupView groupView) {
					var groupSO = groupView.GetEmptyGroupSO();
					groupView.SetGroupSO(groupSO);

					graphSO.Groups.Add(groupSO);
					AssetDatabase.AddObjectToAsset(groupSO, graphSO);

					foreach (var groupElement in groupView.containedElements) {
						if (groupElement is NSNodeViewBase nodeView2) {
							var nodeSO = nodeView2.GetEmptyNodeSO();

							idToNodeSOs.Add(nodeView2.ID, nodeSO);
							viewToSOs.Add(nodeView2, nodeSO);
							groupSO.InGroupNodes.Add(nodeSO);
							nodeSO.BelongedGroup = groupSO;
							AssetDatabase.AddObjectToAsset(nodeSO, graphSO);
						}
					}
				}
			}

			foreach (var nodeView in nodes.Cast<NSNodeViewBase>()) {
				nodeView.SetNodeSO(viewToSOs[nodeView], this, idToNodeSOs);
			}
		}
	  #endregion

	  #region Clear Method
		public void ClearGraphView() {
			_nameErrorCount = 0;
			_ungroupedNodeViews.Clear();
			_groupedNodeViews.Clear();
			DeleteElements(graphElements);
		}
	  #endregion

	  #region MiniMap Method
	  	public void ToggleMiniMap() {
			_miniMap.visible = !_miniMap.visible;
			if (_miniMap.visible) {
				_miniMap.SetPosition(new(
					_graphEditor.position.width - DEFAULT_MINIMAP_WIDTH, 
					_graphEditor.position.height - DEFAULT_MINIMAP_HEIGHT, 
					DEFAULT_MINIMAP_WIDTH, 
					DEFAULT_MINIMAP_HEIGHT));
			}
		}
	  #endregion

	#endregion

	  #region Overriden Callbacks
		private void OverrideCallbacks() {
			OverrideDeleteSelection();
			OverrideGroupChangeCallbacks();
		}
		private void OverrideDeleteSelection() {
			deleteSelection = (operationName, AskUser) => {
				/* INIT ELEMENTS TO REMOVE */
				List<NSNodeViewBase> nodeViewsToDelet = new();
				List<NSGroupView> groupViewsToDelet = new();
				List<Edge> edgesToDelet = new();
				foreach (var element in selection.Cast<GraphElement>()) {
					if (element is NSNodeViewBase nodeView) nodeViewsToDelet.Add(nodeView);	
					else if (element is NSGroupView groupView) groupViewsToDelet.Add(groupView);
					else if (element is Edge edge) edgesToDelet.Add(edge);
				}

				/* DELETE EDGES */
				DeleteElements(edgesToDelet);
				/* DELETE NODEVIEWS */
				foreach (var nodeView in nodeViewsToDelet) {
					if (nodeView.GroupView != null) {
						nodeView.GroupView.RemoveElement(nodeView);
					}
					RemoveUngroupedNodeView(nodeView);
					nodeView.DisconnectAllPorts();
					RemoveElement(nodeView);
				}
				/* DELETE GROUPVIEWS */
				foreach (NSGroupView groupView in groupViewsToDelet) {
					List<NSNodeViewBase> nodeViewsInGroup = new();
					foreach (var element in groupView.containedElements) 
						if (element is NSNodeViewBase nodeView)
							nodeViewsInGroup.Add(nodeView);
					groupView.RemoveElements(nodeViewsInGroup);
					RemoveElement(groupView);
				}
			};
		}
		private void OverrideGroupChangeCallbacks() {
			elementsAddedToGroup = (group, elements) => {
				var groupView = group as NSGroupView;
				foreach (var element in elements) {
					if (element is not NSNodeViewBase) continue;
					var nodeView = element as NSNodeViewBase;
					RemoveUngroupedNodeView(nodeView);
					nodeView.GroupView = groupView;
					AddGroupedNodeView(nodeView, groupView.title);
				}
			};
			elementsRemovedFromGroup = (group, elements) => {
				var groupView = group as NSGroupView;
				foreach (var element in elements) {
					if (element is not NSNodeViewBase) continue;
					var nodeView = element as NSNodeViewBase;
					nodeView.GroupView = null;
					RemoveGroupedNodeView(nodeView, groupView.title);
					AddUngroupedNodeView(nodeView);
				}
			};
			groupTitleChanged = (group, newTitle) => {
				var groupView = group as NSGroupView;
				foreach (var element in groupView.containedElements) {
					if (element is NSNodeViewBase nodeView) {
						RemoveGroupedNodeView(nodeView, groupView.OldTitle);
						AddGroupedNodeView(nodeView, newTitle);
					}
				}
				groupView.OldTitle = newTitle;
			};
		}
	  #endregion

	  #region Overriden Methods
		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter){
			List<Port> compatiblePorts = new();
			ports.ForEach(port => {
				if (startPort.node == port.node) return;
				if (startPort.direction == port.direction) return;
				compatiblePorts.Add(port);
			});
			return compatiblePorts;
		}
	  #endregion

	  #region Visual Elements Creation
	  	/// <summary>
		/// 创建 nodeView 并初始化组件（调用Draw），并添加到 graphView 中
		/// </summary>
		public NSNodeViewBase CreateNodeView(Type type, Vector2 position, string nodeViewName = null) {
			NSNodeViewBase nodeView = Activator.CreateInstance(type) as NSNodeViewBase;
			nodeView.Generate(this, type, position, nodeViewName);
			nodeView.Draw();
			AddUngroupedNodeView(nodeView);
			AddElement(nodeView);
			return nodeView;
		}
		public NSGroupView CreateGroupView(Vector2 position, string groupViewTitle = null) {
			NSGroupView groupView = new(position, groupViewTitle);
			AddElement(groupView);
			// 添加当前选中的节点到当前 groupView
			foreach (var element in selection) {
				if (element is NSNodeViewBase nodeView) {
					groupView.AddElement(nodeView);
				}
			}
			return groupView;
		}
	  #endregion

	  #region Repeated Elements Add/Remove
		public void AddUngroupedNodeView(NSNodeViewBase nodeView) {
			string nodeViewName = nodeView.NodeViewName.ToLower();
			// 如果还没有这个名字的 nodeView，就创建一个新的错误信息
			if (!_ungroupedNodeViews.ContainsKey(nodeViewName)) {
				NodeViewErrorData errorData = new();
				errorData.NodeViews.Add(nodeView);
				_ungroupedNodeViews.Add(nodeViewName, errorData);
				return;
			}
			// 如果有重名 nodeView 的话
			Color color = _ungroupedNodeViews[nodeViewName].ErrorColor.Color;
			var nodeViews = _ungroupedNodeViews[nodeViewName].NodeViews;
			nodeViews.Add(nodeView);
			nodeView.SetErrorStyle(color);
			if (nodeViews.Count == 2) {
				++NameErrorCount;
				nodeViews[0].SetErrorStyle(color);
			}
		}
		public void RemoveUngroupedNodeView(NSNodeViewBase nodeView) {
			string nodeViewName = nodeView.NodeViewName.ToLower();
			var nodeViews = _ungroupedNodeViews[nodeViewName].NodeViews;
			nodeViews.Remove(nodeView);
			nodeView.RemoveErrorStyle();
			if (nodeViews.Count == 1) {
				--NameErrorCount;
				nodeViews[0].RemoveErrorStyle();
			}
			else if (nodeViews.Count == 0)
				_ungroupedNodeViews.Remove(nodeViewName);
		}
		public void AddGroupedNodeView(NSNodeViewBase nodeView, string groupViewTitle) {
			string nodeViewName = nodeView.NodeViewName;
			if (!_groupedNodeViews.ContainsKey(groupViewTitle)) {
				_groupedNodeViews.Add(groupViewTitle, new());
			}
			if (!_groupedNodeViews[groupViewTitle].ContainsKey(nodeViewName)) {
				NodeViewErrorData errorData = new();
				errorData.NodeViews.Add(nodeView);
				_groupedNodeViews[groupViewTitle].Add(nodeViewName, errorData);
				return;
			}
			// 如果 nodeView 重名了
			Color color = _groupedNodeViews[groupViewTitle][nodeViewName].ErrorColor.Color;
			var nodeViews = _groupedNodeViews[groupViewTitle][nodeViewName].NodeViews;
			nodeViews.Add(nodeView);
			nodeView.SetErrorStyle(color);
			if (nodeViews.Count == 2) {
				++NameErrorCount;
				nodeViews[0].SetErrorStyle(color);
			}
		}
		public void RemoveGroupedNodeView(NSNodeViewBase nodeView, string groupViewTitle) {
			nodeView.RemoveErrorStyle();
			string nodeViewName = nodeView.NodeViewName;
			var nodeViews = _groupedNodeViews[groupViewTitle][nodeViewName].NodeViews;
			nodeViews.Remove(nodeView);
			if (nodeViews.Count == 1) {
				--NameErrorCount;
				nodeViews[0].RemoveErrorStyle();
			}
			else if (nodeViews.Count == 0) {
				_groupedNodeViews[groupViewTitle].Remove(nodeViewName);
				if (_groupedNodeViews[groupViewTitle].Count == 0) {
					_groupedNodeViews.Remove(groupViewTitle);
				}
			}
		}
	  #endregion 
	
	  #region Add Components
	  	/// <summary>
		/// 添加相关组件：Manipulators，GridBackground，ussStyle
		/// </summary>
		private void AddComponents() {
			AddManipulators();
			AddGridBackground();
			AddStyles();
			AddMiniMap();
		}
		private void AddManipulators() {
			SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());

			AddCreateMenu();
		}
		private void AddCreateMenu() {
			/* Node View Menu Creation */
			foreach (var type in DerivedNodeViewTypes) {
				AddNodeViewCreateMenu(type);
			}
			/* Group View Menu Creation */
			AddGroupViewCreateMenu();
		}
		private void AddNodeViewCreateMenu(Type type) {
			this.AddManipulator(new ContextualMenuManipulator( 
				menuEvt => menuEvt.menu.AppendAction(
					$"Add {type.Name}", 
					evt => AddElement(CreateNodeView(type, GetLocalMousePos(evt.eventInfo.mousePosition)))
				)
			));
		}
		private void AddGroupViewCreateMenu() {
			this.AddManipulator(new ContextualMenuManipulator( 
				menuEvt => menuEvt.menu.AppendAction(
					$"Add Group", 
					evt => AddElement(CreateGroupView(GetLocalMousePos(evt.eventInfo.mousePosition)))
				)
			));
		}
		private void AddGridBackground() {
			GridBackground gridBackground = new();
			gridBackground.StretchToParentSize();
			Insert(0, gridBackground);
		}
		private void AddStyles() {
			this.AddStyleSheets("/NS Graph Editor/GraphView.uss", "/NS Graph Editor/NodeView.uss");
		}
		private void AddMiniMap() {
			_miniMap = new();
			Add(_miniMap);
			ToggleMiniMap();
		}
	  #endregion

	  #region Utilities
		private Vector2 GetLocalMousePos(Vector2 mousePos) {
			Vector2 localMousePos = contentViewContainer.WorldToLocal(mousePos);
			return localMousePos;
		}
		private void InitDerivedNodeViewTypes() {
			DerivedNodeViewTypes = new();
			Type baseType = typeof(NSNodeViewBase);
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				Type[] types = assembly.GetTypes();
				foreach (Type type in types) {
					if (type != baseType && baseType.IsAssignableFrom(type)) {
						DerivedNodeViewTypes.Add(type);
					}
				}
			}
		}
	  #endregion 
	
	}
}
