namespace GameLogic.Model.Element.Vill {
	[System.Serializable]
	public class LogicImplerSave {
		public ulong ID;
		public string FirstName;
		public string LastName;
		public Coord Coord;
		public TaskRunnerSave TaskRunnerSave;
		public RepoBuffHelperSave RepoBuffHelperSave;
		public ExpHelperSave ExpHelperSave;
		public VitHelperSave VitHelperSave;
		public BondArchHelperSave BondArchHelperSave;

		public LogicImplerSave Clone() {
			return new() {
				ID = ID,
				FirstName = FirstName,
				LastName = LastName,
				Coord = Coord,
				TaskRunnerSave = TaskRunnerSave.Clone(),
				RepoBuffHelperSave = RepoBuffHelperSave.Clone(),
				ExpHelperSave = ExpHelperSave.Clone(),
				VitHelperSave = VitHelperSave.Clone(),
				BondArchHelperSave = BondArchHelperSave.Clone()
			};
		}
	}
}