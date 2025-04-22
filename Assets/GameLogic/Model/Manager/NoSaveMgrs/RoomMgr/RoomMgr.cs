using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public sealed class RoomMgr : IClearMgr {
		private RoomMgr() {
			EventSystem.AddListener<VillLogicBase>((int)ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchAdded_A_1, OnArchAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<ArchLogicBase>((int)ModelEvt.ArchDestroyed_A_1, OnArchDestroyed, NSFrame.EventType.Model);
		}
		public static RoomMgr Inst { get; } = new();

		private readonly HashSet<ulong> _availableCottageIDs = new();
		private readonly HashSet<ulong> _occupiedCottageIDs = new();

		private void OnVillAdded(VillLogicBase vill) {
			if (!vill.IsHomeless || vill.TaskRunner.CurTaskType == TaskType.Leave) { return; }
			FindRoomForVill_private(vill);
		}
		private void OnArchAdded(ArchLogicBase arch) {
			if (arch is not CottageLogic cottage) return;
			if (cottage.BondedVillCount == cottage.Lconfig.MaxContain) {
				_occupiedCottageIDs.Add(cottage.ID);
			} else {
				_availableCottageIDs.Add(cottage.ID);
			}
		}
		private void OnArchDestroyed(ArchLogicBase arch) {
			if (arch is not CottageLogic cottage) return;
			if (_occupiedCottageIDs.Contains(cottage.ID)) {
				_occupiedCottageIDs.Remove(cottage.ID);
			}
			if (_availableCottageIDs.Contains(cottage.ID)) {
				_occupiedCottageIDs.Remove(cottage.ID);
			}
		}
		private bool FindRoomForVill_private(VillLogicBase vill) {
			if (_availableCottageIDs.Count == 0) {
				Debug.LogWarning("<RoomMgr>: No Available Cottage When Vill Added.");
				return false;
			}
			var ctgID = _availableCottageIDs.First();
			vill.SetHomeID(ctgID);
			var cottage = WorldMgr.Inst.FindArch(ctgID);

			cottage.TryBondVill(vill.ID);
			if (cottage.BondedVillCount == cottage.Lconfig.MaxContain) {
				_availableCottageIDs.Remove(ctgID);
				_occupiedCottageIDs.Add(ctgID);
			}
			return true;
		}

		#region PublicMethods
		public bool FindRoomForVill(VillLogicBase vill) {
			return FindRoomForVill_private(vill);
		}

		public void ClearMgr() {
			_availableCottageIDs.Clear();
			_occupiedCottageIDs.Clear();
		}
		#endregion

	}
}