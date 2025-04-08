using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class MainPanel : PanelBase {
		[Header("挂载")]
		
		public ArchTagPanel ArchTagPanel;
		public GroupMgr GroupMgr;

		public override void OnClose() {
			GroupMgr.OnClose();
		}
		public override void OnShow() {
			GroupMgr.SetCurGroupType(GroupType.Arch, ArchType.Cottage);
		}

		

		#region PublicMethods
		
		#endregion
	}
}