using System.Collections.Generic;

namespace GameLogic {
	[System.Serializable]
	public abstract class ArchSaveBase {
		public ArchType ArchType;
		public ulong ID;
		public OL OL;
		public int Level;
		public RTList<float> ProdBuffs;
		public RTList<float> ConsBuffs;
		public List<ulong> BookedPosVillIDs;
		public List<ulong> InVillIDs;

		protected abstract ArchSaveBase GetDerivedClone();
		public ArchSaveBase Clone() {
			var save = GetDerivedClone();
				save.ArchType = ArchType;
				save.ID = ID;
				save.OL = OL;
				save.Level = Level;
				save.ProdBuffs = ProdBuffs.Clone();
				save.ConsBuffs = ConsBuffs.Clone();
				save.BookedPosVillIDs = new(BookedPosVillIDs);
				save.InVillIDs = new(InVillIDs);
			return save;
		}
	}
}