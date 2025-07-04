using UnityEngine;

namespace OldGameLogic.Model.Element.Vill {
	[System.Serializable]
	public class BondArchHelperSave {
		[HideInInspector] public ulong HomeID;
		[HideInInspector] public ulong BondedWorkArchID;
		public BondArchHelperSave Clone() {
			return new() {
				HomeID = HomeID,
				BondedWorkArchID = BondedWorkArchID,
			};
		}
	}
}