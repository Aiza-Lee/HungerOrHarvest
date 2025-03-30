using System.Collections.Generic;

namespace GameLogic
{
	public class CottageLogic : ArchLogicBase {
		public override ArchType ArchType => ArchType.Cottage;

		#region Injection
		public void AddBondedVill(ulong vID) { _bondedVillIDs.Add(vID); }
		#endregion

		private List<ulong> _bondedVillIDs = new();
		public int BondedVillCount => _bondedVillIDs.Count;

		protected override void DerivedInitFromSave(ArchSaveBase save) {
			var sv = save as CottageSave;
			_bondedVillIDs = sv.BondedVillIDs;
		}
		protected override ArchSaveBase GetDerivedSave() {
			return new CottageSave() {
				BondedVillIDs = new(_bondedVillIDs),
			};
		}
	}
}