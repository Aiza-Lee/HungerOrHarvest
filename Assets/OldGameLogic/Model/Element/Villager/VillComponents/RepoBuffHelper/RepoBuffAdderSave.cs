namespace OldGameLogic.Model.Element.Vill {
	[System.Serializable]
	public class RepoBuffAdderSave {
		public RepoBuffType RepoBuffType;
		public RTListSave<float> Buffs;
		public int Ticks;
		public RepoBuffAdderSave Clone() {
			return new() {
				RepoBuffType = RepoBuffType,
				Buffs = Buffs.Clone(),
				Ticks = Ticks,
			};
		}
	}
}