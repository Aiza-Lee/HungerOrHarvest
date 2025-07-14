using System;
using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Utils;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Elements.Vill;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.WorldVill {
	public class MainPanel : PanelBase, IRegisterUiMgr {
		[Header("挂载")]
		[SerializeField] private OptionTagMgr _optionTagMgr;
		[SerializeField] private GroupMgr _groupMgr;

		public GroupType? CurGroupType { get; private set; } = null;
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

		/// <summary>
		/// 设置当前组的显示组的类型
		/// </summary>
		/// <param name="groupType">组的类型</param>
		/// <param name="archType">如果是工作建筑，对应的建筑类型</param>
		private void SetGroupTypeImpl(GroupType groupType, ArchType? archType) {
			CurGroupType = groupType;
			CurArchType = archType;
			_groupMgr.SetCurGroupType(groupType, archType);
			_optionTagMgr.SetAllTagDirty();
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


		/// <summary>
		/// 将所有选中的村民卡片移动到目标位置，并逐渐缩小到 0 后销毁
		/// </summary>
		/// <param name="target">目标位置</param>
		/// <param name="customAction">在移除后的自定义操作</param>
		private void DoCardsTransfer(RectTransform target, Action<VillCard> customAction = null) {
			// 虽然设计之初并没有要求group的view响应村民工作变化的事件，然而为了保险起见
			// 即在设置工作的时候可能会调用 这里的 RemoveSelectedVillId 方法，从而改变用于枚举的_selectedVillIds
			List<VillCard> tmpVillCards = new(_selectedCards);
			foreach (var villCard in tmpVillCards) {
				villCard.BelongedGroup.RemoveEle(villCard);
				villCard.TransferTo(target, (vc) => vc.LogicDestroy());
				customAction?.Invoke(villCard);
			}
		}
		/// <summary>
		/// 当点击了OptionTag的时候调用，由OptionTag自己触发，调用这个方法，并把自己作为参数传过来
		/// </summary>
		/// <param name="tag">方法调用者</param>
		public void OnOptionTagClicked(OptionTag tag) {
			var clickedGroupType = tag.TagGroupType;
			var clickedArchType = tag.TagArchType;

			if (Input.GetKey(KeyCode.LeftControl)) {
				switch (clickedGroupType) {
					case GroupType.Arch: {
							DoCardsTransfer(tag.RectTrans, vc => {
								var arch = ArchQueryAPI.GetBondableWorkArch((ArchType) clickedArchType);
								ArchRequestAPI.RequestBondToVill(arch, vc.TargetVill);
								VillRequestAPI.RequestBondToArch(vc.TargetVill, arch);
							});
							break;
						}
					case GroupType.Workless: {
							DoCardsTransfer(tag.RectTrans, vc => {
								var arch = VillQueryAPI.GetWorkArchGid(vc.TargetVill).ToEntity();
								ArchRequestAPI.RequestDisbondVill(arch, vc.TargetVill);
								VillRequestAPI.RequestDisbondArch(vc.TargetVill, arch);
							});
							break;
						}
					case GroupType.Homeless: {
							DoCardsTransfer(tag.RectTrans, vc => {
								var arch = VillQueryAPI.GetHomeArchGid(vc.TargetVill).ToEntity();
								ArchRequestAPI.RequestDisbondVill(arch, vc.TargetVill);
								VillRequestAPI.RequestDisbondArch(vc.TargetVill, arch);
							});
							break;
						}
					case GroupType.Home: {
							DoCardsTransfer(tag.RectTrans, (vc) => {
								var arch = ArchQueryAPI.GetBondableWorkArch(ArchType.Cottage);
								ArchRequestAPI.RequestBondToVill(arch, vc.TargetVill);
								VillRequestAPI.RequestBondToArch(vc.TargetVill, arch);
							});
							break;
						}
				}
			} else {
				SetGroupTypeImpl(clickedGroupType, clickedArchType);
			}
			// 无论是否按住 ctrl 都应该清空选中的村民
			_selectedCards.Clear();
		}

		#endregion
	}
}