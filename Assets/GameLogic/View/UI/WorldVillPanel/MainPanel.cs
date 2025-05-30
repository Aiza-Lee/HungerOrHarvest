using System.Collections.Generic;
using GameLogic.Controller;
using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class MainPanel : PanelBase {
		[Header("挂载")]
		[SerializeField] private OptionTagMgr _optionTagMgr;
		[SerializeField] private GroupMgr _groupMgr;

		public GroupType CurGroupType { get; private set; } = GroupType.None;
		public ArchType? CurArchType { get; private set; } = null;
		private readonly List<VillCard> _selectedCards = new();

		/// <summary>
		/// 当前是否按下 Ctrl 键
		/// </summary>
		private bool _useControlKey;

		public int SelectedVillCount => _selectedCards.Count;

		public override void OnClose() {
			_optionTagMgr.OnClose();
			_groupMgr.OnClose();
		}
		public override void OnShow() {
			_optionTagMgr.OnShow();
			_groupMgr.OnShow();
		}
		private void Update() {
			if (Input.GetKey(KeyCode.LeftControl)) {
				if (_useControlKey == false) {
					_useControlKey = true;
					_optionTagMgr.SetAllTagDirty();
				}
			} else {
				if (_useControlKey == true) {
					_useControlKey = false;
					_optionTagMgr.SetAllTagDirty();
				}
			}
		}
		

		#region PublicMethods
		
		public void SelectCard(VillCard vc) {
			_selectedCards.Add(vc);
			_optionTagMgr.SetAllTagDirty();
		}
		public void DeselectCard(VillCard vc) {
			_selectedCards.Remove(vc);
			_optionTagMgr.SetAllTagDirty();
		}

		public void SetGroupType(GroupType groupType, ArchType? archType = null) {
			CurGroupType = groupType;
			CurArchType = archType;
			_groupMgr.SetCurGroupType(groupType, archType);
			_optionTagMgr.SetAllTagDirty();
		}
		public void OnOptionTagClicked(OptionTag tag) {
			var groupType = tag.GroupType;
			var archType = tag.ArchType;
			// if (Input.GetKey(KeyCode.LeftControl)) {
			// 	if (groupType == GroupType.Arch) {

			// 		// // 虽然设计之初并没有要求group的view响应村民工作变化的事件，然而为了保险起见
			// 		// // 即在设置工作的时候可能会调用 这里的 RemoveSelectedVillId 方法，从而改变用于枚举的_selectedVillIds
			// 		// List<VillCard> tmpVillIDs = new(_selectedVillIds);
			// 		// foreach (var vc in tmpVillIDs) {
			// 		// 	vc.BelongedGroup.RemoveEle(vc);
			// 		// 	vc.TransferTo(tag.RectTrans, (vc) => vc.Clear());
			// 		// 	CmdRunner.Run($"/vill-bond-arch {vc.AttachedVillID} {WorldMgr.Inst.FindWorkForVill(archType)}");
			// 		// }

			// 	} else if (groupType == GroupType.Workless) {
			// 		List<VillCard> tmpVillIDs = new(_selectedCards);
			// 		foreach (var vc in tmpVillIDs) {
			// 			vc.BelongedGroup.RemoveEle(vc);
			// 			vc.TransferTo(tag.RectTrans, (vc) => vc.Clear());
			// 			CmdRunner.Run($"/vill-disbond-workarch {vc.AttachedVillID}");
			// 		}
			// 	}
			// } else {
				SetGroupType(groupType, archType);
			// }
			// 无论是否按住 ctrl 都应该清空选中的村民
			_selectedCards.Clear();
		}

		#endregion
	}
}