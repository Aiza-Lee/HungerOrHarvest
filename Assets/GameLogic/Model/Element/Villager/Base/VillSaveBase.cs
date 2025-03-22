namespace GameLogic
{
	[System.Serializable]
	public abstract class VillSaveBase {
		public ulong ID;
		public VillType VillType;
		public string FirstName, LastName;
		public JTList<int> JobLevel;
		public JTList<float> JobExps;
		public RTList<float> ConsBuffs;
		public RTList<float> ProdBuffs;
		public Coord Coord;
		public StaMachineSave StaMachine;
		

		protected abstract VillSaveBase GetDerivedClone();
		public VillSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID = ID;
				save.VillType = VillType;
				save.FirstName = FirstName;
				save.LastName = LastName;
				save.JobLevel = JobLevel.Clone();
				save.JobExps = JobExps.Clone();
				save.ConsBuffs = ConsBuffs.Clone();
				save.ProdBuffs = ProdBuffs.Clone();
				save.Coord = Coord;
				save.StaMachine = StaMachine.Clone();
			return save;
		}
	}
}