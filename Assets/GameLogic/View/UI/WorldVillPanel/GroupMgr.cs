namespace GameLogic.View.UI.WorldVillPanel 
{
	public class GroupMgr : ScrollGroupLayoutBase {

		#region PublicMethods

		public void OnShow() {}
		public void OnClose() {
			Clear();
		}

		public void SetCurGroupType(GroupType groupType, ArchType? archType = null) {
			// if (_curGroupType == groupType && _curArchType == archType) { return; }
			Clear();
			// 如果是展示arch的group
			if (groupType == GroupType.Arch && archType != null) {
				// 获取对应类型的全部建筑的 View
				var archViews = WorldViewMgr.Inst.GetAllArchViews((ArchType) archType);
				archViews.Sort((a, b) => a.Logic.Coord.X.CompareTo(b.Logic.Coord.X));

				// 为每一个建筑实例 创建一个 Group
				foreach (var archView in archViews) {
					var group = VillGroupFactory.Inst.Create(archView.Logic);
					AddEle(group);
					group.RearrangeEle();
				}
			} else { // 如果是展示 Homeless 或者 Workless 的group
				var group = VillGroupFactory.Inst.Create(groupType);
				AddEle(group);
				group.RearrangeEle();
			}
		}

		#endregion
	}
}