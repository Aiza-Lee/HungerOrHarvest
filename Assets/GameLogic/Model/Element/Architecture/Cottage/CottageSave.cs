using System;
using System.Collections.Generic;

namespace GameLogic
{
	[Serializable]
	public class CottageSave : ArchSaveBase {
		public List<ulong> BondedVillIDs;
		protected override ArchSaveBase GetDerivedClone() {
			return new CottageSave() {
				BondedVillIDs = new(BondedVillIDs),
			};
		}
	}
}