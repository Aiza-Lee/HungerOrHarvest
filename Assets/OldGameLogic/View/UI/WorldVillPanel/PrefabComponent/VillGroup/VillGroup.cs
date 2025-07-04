using System;
using System.Collections.Generic;
using OldGameLogic.Model.Element.Arch;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.View.UI.WorldVillPanel
{
	public class VillGroup : GroupLayoutBase, IGroupLayoutEle {

		private void GenerateVillCards(List<ulong> ids) {
			foreach (var id in ids) {
				var card = VillCardFactory.Inst.Create(id);
				AddEle(card);
			}
		}

		#region Injection
		/// <summary>
		/// 初始化group，group类型为建筑
		/// </summary>
		/// <param name="arch">建筑</param>
		public void SetGroupInfo(ArchLogicBase arch) {
			GenerateVillCards(arch.BondedVillIDs);
		}
		/// <summary>
		/// 初始化group，group类型为非建筑
		/// </summary>
		/// <param name="groupType">group的类型</param>
		public void SetGroupInfo(GroupType groupType) {
			if (groupType == GroupType.Homeless) {
				GenerateVillCards(WorldMgr.Inst.FindVillIDs((v) => v.IsHomeless));
			} else if (groupType == GroupType.Workless) {
				GenerateVillCards(WorldMgr.Inst.FindVillIDs((v) => !v.IsHomeless && v.IsWorkless));
			}
		}
 		#endregion

		#region PublicMethods
		public override void SetLength(float width) {
			base.SetLength(width);
			OnDirty?.Invoke();
		}
		#endregion

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public RectTransform RectTrans => _rectTrans;
		public float Height => _rectTrans.rect.height;
		public float EleSize => base.EleContainerSize;
		public event Action OnDirty;
		public void SetPos(float pos) {
			_rectTrans.offsetMax = new(pos + EleSize, _rectTrans.offsetMax.y);
			_rectTrans.offsetMin = new(pos, _rectTrans.offsetMin.y);
		}
		public void OnAddedToGroup() {
			_rectTrans.offsetMin = new(0, 0);
			_rectTrans.offsetMax = new(0, 0);
			RearrangeEle();
		}
		public void LogicDestroy() {
			base.Clear();
			PoolSystem.PushGO(gameObject);
		}
		#endregion
	}
}