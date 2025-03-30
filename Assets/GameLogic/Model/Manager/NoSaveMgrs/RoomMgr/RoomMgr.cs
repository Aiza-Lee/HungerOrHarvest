using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public sealed class RoomMgr : IClearMgr {
		private RoomMgr() {
			EventSystem.AddListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
		}
		~RoomMgr() {
			EventSystem.RemoveListener<VillLogicBase>((int)LogicEvt.VillAdded_V, OnVillAdded);
			EventSystem.RemoveListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
		}
		public static RoomMgr Inst { get; } = new();

		private readonly HashSet<ulong> _availableCottageIDs = new();
		private readonly HashSet<ulong> _occupiedCottageIDs = new();

		private void OnVillAdded(VillLogicBase vill) {
			if (vill.HomeID != 0 || vill.TaskRunner.CurTaskType == TaskType.Leave) { return; }
			FindRoomForVill(vill);
		}
		private void OnArchAdded(ArchLogicBase arch) {
			if (arch is not CottageLogic cottage) return;
			if (cottage.BondedVillCount == cottage.Lconfig.MaxContain) {
				_occupiedCottageIDs.Add(cottage.ID);
			} else {
				_availableCottageIDs.Add(cottage.ID);
			}
		}

		#region PublicMethods
		public bool FindRoomForVill(VillLogicBase vill) {

			if (_availableCottageIDs.Count == 0) {
				Debug.LogWarning("<RoomMgr>: No Available Cottage When Vill Added.");
				return false;
			}
			var ctgID = _availableCottageIDs.First();
			vill.SetHomeID(ctgID);
			var cottage = WorldMgr.Inst.FindArch(ctgID) as CottageLogic;

			cottage.AddBondedVill(vill.ID);
			if (cottage.BondedVillCount == cottage.Lconfig.MaxContain) {
				_availableCottageIDs.Remove(ctgID);
				_occupiedCottageIDs.Add(ctgID);
			}
			return true;
		}

		public void ClearMgr() {
			_availableCottageIDs.Clear();
			_occupiedCottageIDs.Clear();
		}
		#endregion

	}
}