using System.Linq;

namespace GameLogic.View.UI.WorldVillPanel 
{
	public class GroupMgr : ScrollGroupLayoutBase {

		#region PublicMethods

		public void OnShow() {}
		public void OnClose() {
			Clear();
		}

		/// <summary>
		/// 设置当前显示的组类型
		/// </summary>
		/// <param name="groupType">组的类型</param>
		/// <param name="archType">如果组是Arch，对应的建筑类型</param>
		public void SetCurGroupType(GroupType groupType, ArchType? archType) {
			Clear();
			switch (groupType) {
				case GroupType.Arch: {
						var archs = WorldMgr.Inst.FindAllArchs((ArchType) archType).ToList();
						archs.Sort((a, b) => a.Coord.X.CompareTo(b.Coord.X));
						foreach (var arch in archs) {
							var group = VillGroupFactory.Inst.Create(arch);
							AddEle(group);
						}
						break;
					}
				case GroupType.Home: {
						var homes = WorldMgr.Inst.FindAllArchs(ArchType.Cottage).ToList();
						homes.Sort((a, b) => a.Coord.X.CompareTo(b.Coord.X));
						foreach (var home in homes) {
							var group = VillGroupFactory.Inst.Create(home);
							AddEle(group);
						}
						break;
					}
				case GroupType.Homeless:
				case GroupType.Workless: {
						var group = VillGroupFactory.Inst.Create(groupType);
						AddEle(group);
						break;
					}
			}
		}

		#endregion
	}
}