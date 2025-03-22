using System.Collections.Generic;
using NSFrame;
using UnityEngine.UIElements;
using System.Linq;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using GameLogic;

namespace ExtendFrame {
	public class ProjectNodeView : NSNodeViewBase {
		public NSPort OutputPort;
		public string Tag, Text;
		private readonly List<NSPairRef<RepoType, ulong>> _demands = new();
		private readonly List<NSPairRef<ArchType, int>> _unlocks = new();
		private readonly List<NSPairRef<ArchType, int>> _buffs = new();
		public TextField TextTF, TagTF;

		private VisualElement _demandFoldout, _unlockFoldout, _buffFoldout;
		private static readonly List<string> _RepositoryNames, _ArchNames;

		static ProjectNodeView() {
			_RepositoryNames = Enum.GetNames(typeof(RepoType)).ToList();
			_ArchNames = Enum.GetNames(typeof(ArchType)).ToList();
		}

		private void AddNewDemand() {
			AddDemand(new(RepoType.Villager, 0));
		}
		public void AddDemand(NSPairRef<RepoType, ulong> demand) {
			_demands.Add(demand);

			var demandContainer = new VisualElement();
			demandContainer.style.flexDirection = FlexDirection.Row;

			/* ENUM DROP DOWN */
			var enumDropDown = new DropdownField("", _RepositoryNames, demand.Key.GetHashCode()) {
				value = demand.Key.ToString()
			};
			enumDropDown.RegisterValueChangedCallback( evt => {
				demand.Key = (RepoType)System.Enum.Parse(typeof(RepoType), evt.newValue);
			} );
			demandContainer.Add(enumDropDown);

			/* NUMBER FIELD */
			var numField = GraphViewUtility.CreateTextField(demand.Value.ToString(), null, evt => {
				demand.Value = ulong.Parse(evt.newValue);
			});
			demandContainer.Add(numField);

			/* DELETE BUTTON */
			var deleteButton = GraphViewUtility.CreateButton("X", () => {
				_demands.Remove(demand);
				_demandFoldout.Remove(demandContainer);
			});
			deleteButton.style.marginLeft = StyleKeyword.Auto;
			demandContainer.Add(deleteButton);
			
			_demandFoldout.Add(demandContainer);
		}

		private void AddNewUnlock() {
			AddUnlock(new(ArchType.Cottage, 0));
		}
		public void AddUnlock(NSPairRef<ArchType, int> unlock) {
			_unlocks.Add(unlock);
			var unlockContainer = new VisualElement();
			unlockContainer.style.flexDirection = FlexDirection.Row;

			/* ENUM DROP DOWN */
			var enumDropDown = new DropdownField("", _ArchNames, unlock.Key.GetHashCode()) {
				value = unlock.Key.ToString()
			};
			enumDropDown.RegisterValueChangedCallback( evt => {
				unlock.Key = (ArchType)System.Enum.Parse(typeof(ArchType), evt.newValue);
			} );
			unlockContainer.Add(enumDropDown);

			/* NUMBER FIELD */
			var numField = GraphViewUtility.CreateTextField(unlock.Value.ToString(), null, evt => {
				unlock.Value = int.Parse(evt.newValue);
			});
			unlockContainer.Add(numField);

			/* DELETE BUTTON */
			var deleteButton = GraphViewUtility.CreateButton("X", () => {
				_unlocks.Remove(unlock);
				_unlockFoldout.Remove(unlockContainer);
			});
			deleteButton.style.marginLeft = StyleKeyword.Auto;
			unlockContainer.Add(deleteButton);
			
			_unlockFoldout.Add(unlockContainer);
		}

		private void AddNewBuff() {
			AddBuff(new(ArchType.Cottage, 0));
		}
		public void AddBuff(NSPairRef<ArchType, int> buff) {
			_buffs.Add(buff);
			var buffContainer = new VisualElement();
			buffContainer.style.flexDirection = FlexDirection.Row;

			/* ENUM DROP DOWN */
			var enumDropDown = new DropdownField("", _ArchNames, buff.Key.GetHashCode()) {
				value = buff.Key.ToString()
			};
			enumDropDown.RegisterValueChangedCallback( evt => {
				buff.Key = (ArchType)System.Enum.Parse(typeof(ArchType), evt.newValue);
			} );
			buffContainer.Add(enumDropDown);

			/* NUMBER FIELD */
			var numField = GraphViewUtility.CreateTextField(buff.Value.ToString(), null, evt => {
				buff.Value = int.Parse(evt.newValue);
			});
			buffContainer.Add(numField);

			/* DELETE BUTTON */
			var deleteButton = GraphViewUtility.CreateButton("X", () => {
				_buffs.Remove(buff);
				_buffFoldout.Remove(buffContainer);
			});
			deleteButton.style.marginLeft = StyleKeyword.Auto;
			buffContainer.Add(deleteButton);
			
			_buffFoldout.Add(buffContainer);
		}

		public override void DesignExtensionContainer() {
			var customDataContainer = new VisualElement();

			/* TAG FOLDOUT */
			var tagFoldout = GraphViewUtility.CreateFoldOut("Tag");
			TagTF = GraphViewUtility.CreateTextField(Tag, null, evt => { Tag = evt.newValue; });
			tagFoldout.Add(TagTF);
			customDataContainer.Add(tagFoldout);

			/* TEXT FOLDOUT */
			var textFoldout = GraphViewUtility.CreateFoldOut("节点描述");
			TextTF = GraphViewUtility.CreateTextField(Text, null, evt => { Text = evt.newValue; });
			// TextTF.AddClasses(
			// 	"nsframe-nodeview__textfield",
			// 	"nsframe-nodeview__quote-textfield"
			// );
			TextTF.multiline = true;
			TextTF.style.maxWidth = 400;
			TextTF.style.whiteSpace = WhiteSpace.Normal;
			TextTF.Children().First().style.overflow = Overflow.Visible;
			textFoldout.Add(TextTF);
			customDataContainer.Add(textFoldout);

			/* DEMAND FOLDOUT */
			_demandFoldout = GraphViewUtility.CreateFoldOut("资源需求");
			var addButton = GraphViewUtility.CreateButton("New Demand", AddNewDemand);
			_demandFoldout.Add(addButton);
			customDataContainer.Add(_demandFoldout);

			/* UNLOCK FOLDOUT */
			_unlockFoldout = GraphViewUtility.CreateFoldOut("解锁建筑");
			addButton = GraphViewUtility.CreateButton("New Unlock", AddNewUnlock);
			_unlockFoldout.Add(addButton);
			customDataContainer.Add(_unlockFoldout);

			/* BUFF FOLDOUT */
			_buffFoldout = GraphViewUtility.CreateFoldOut("增益效果");
			addButton = GraphViewUtility.CreateButton("New Buff", AddNewBuff);
			_buffFoldout.Add(addButton);
			customDataContainer.Add(_buffFoldout);

			extensionContainer.Add(customDataContainer);
		}

		public override void DesignOutputContainer() {
			OutputPort = this.CreatePort("out", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
			outputContainer.Add(OutputPort);
		}

		public override NSNodeSOBase GetEmptyNodeSO() {
			return IOUtility.CreateSO<ProjectNodeSO>();
		}

		public override NSNodeViewDataSO GetEmptyNodeViewDataSO() {
			return IOUtility.CreateSO<ProjectNodeViewDataSO>();
		}

		protected override void SetAdditionalNodeSO(NSNodeSOBase nodeSOBase, NSGraphView graphView, Dictionary<string, NSNodeSOBase> nodeSOs) {
			if (nodeSOBase is not ProjectNodeSO) {
				Debug.LogError("NS: NodeSO type wrong.");
				return;
			}
			var nodeSO = nodeSOBase as ProjectNodeSO;

			nodeSO.NodeSOType = NodeViewType.FullName;
			nodeSO.Tag = Tag;
			nodeSO.Text = Text;
			nodeSO.Demands = new();
			foreach (var demand in _demands) { nodeSO.Demands.Add(demand.ToNSPair()); }
			nodeSO.Unlocks = new();
			foreach (var unlock in _unlocks) { nodeSO.Unlocks.Add(unlock.ToNSPair()); }
			nodeSO.Buffs = new();
			foreach (var buff in _buffs) { nodeSO.Buffs.Add(buff.ToNSPair()); }
			nodeSO.NextNodes = new();
			foreach (var edge in graphView.edges) {
				if (edge.output.node == this) {
					nodeSO.NextNodes.Add(nodeSOs[(edge.input.node as NSNodeViewBase).ID]);
				}
			}
		}

		protected override void SetAdditionalViewDataSO(NSNodeViewDataSO nodeViewDataSO) {
			if (nodeViewDataSO is not ProjectNodeViewDataSO) {
				Debug.LogError("NS: NodeViewDataSO type wrong.");
				return;
			}
			var viewDataSO = nodeViewDataSO as ProjectNodeViewDataSO;

			viewDataSO.NodeViewType = NodeViewType.FullName;
			viewDataSO.OutputPortID = OutputPort.ID;
			viewDataSO.Tag = Tag;
			viewDataSO.Text = Text;
			viewDataSO.Demands = new();
			foreach (var demand in _demands) { viewDataSO.Demands.Add(demand.ToNSPair()); }
			viewDataSO.Unlocks = new();
			foreach (var unlock in _unlocks) { viewDataSO.Unlocks.Add(unlock.ToNSPair()); }
			viewDataSO.Buffs = new();
			foreach (var buff in _buffs) { viewDataSO.Buffs.Add(buff.ToNSPair()); }
		}
	}
}