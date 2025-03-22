using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NSFrame
{
	public class NSGroupView : Group {
		const string DEFAULT_GROUP_TITLE = "Group";
		public string OldTitle { get; set; }
		public NSGroupView(Vector2 position, string groupViewTitle) {
			if (string.IsNullOrEmpty(groupViewTitle)) {
				groupViewTitle = DEFAULT_GROUP_TITLE;
			}
			OldTitle = title = groupViewTitle;
			SetPosition(new(position, Vector2.zero));
		}

	  #region Utilities 目前没用
	  	public void SetErrorStyle(Color color) {
			contentContainer.style.borderBottomColor = color;
			contentContainer.style.borderBottomWidth = 2f;
		}
		public void RemoveErrorStyle() {
			contentContainer.style.borderBottomColor = StyleKeyword.Null;
			contentContainer.style.borderBottomWidth = StyleKeyword.Null;
		}
	  #endregion
	
	  #region Save Methods
	  	/// <summary>
		/// 获取 Group View Data SO，group 内的节点会在这个方法中创建 Node View Data SO
		/// </summary>
	  	public NSGroupViewDataSO GetViewDataSO() {
			var groupViewDataSO = IOUtility.CreateSO<NSGroupViewDataSO>();

			groupViewDataSO.InGroupNodeViewDatas = new();
			foreach (var element in containedElements) {
				if (element is NSNodeViewBase nodeView) {
					var nodeViewDataSO = nodeView.GetEmptyNodeViewDataSO();
					nodeView.SetViewDataSO(nodeViewDataSO);

					groupViewDataSO.InGroupNodeViewDatas.Add(nodeViewDataSO);
				}
			}

			groupViewDataSO.Position = GetPosition().position;
			groupViewDataSO.Title = title;
			groupViewDataSO.name = $"Group__{title}";

			return groupViewDataSO;
		}
	  #endregion
	  #region Export Methods
	  	public NSGroupSO GetEmptyGroupSO() {
			return IOUtility.CreateSO<NSGroupSO>();
		}
	  	public void SetGroupSO(NSGroupSO groupSO) {
			groupSO.Title = title;
			groupSO.InGroupNodes = new();
			groupSO.name = $"Group__{title}";
		}
	  #endregion
	}
}

