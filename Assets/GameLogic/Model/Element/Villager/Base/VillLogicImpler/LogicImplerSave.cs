using UnityEngine;
namespace GameLogic.Model.Element.Vill {
	[System.Serializable]
	public class LogicImplerSave {
		[HideInInspector] public ulong ID;
		public string FirstName;
		public string LastName;
		[HideInInspector] public Coord Coord;
		[HideInInspector] public RepoBuffHelperSave RepoBuffHelperSave;
		[HideInInspector] public ExpHelperSave ExpHelperSave;
		public VitHelperSave VitHelperSave;
		public BehaviourHelperSave BehaviourHelperSave;
		[HideInInspector] public BondArchHelperSave BondArchHelperSave;

		public LogicImplerSave Clone() {
			return new() {
				ID = ID,
				FirstName = FirstName,
				LastName = LastName,
				Coord = Coord,
				RepoBuffHelperSave = RepoBuffHelperSave.Clone(),
				ExpHelperSave = ExpHelperSave.Clone(),
				VitHelperSave = VitHelperSave.Clone(),
				BondArchHelperSave = BondArchHelperSave.Clone(),
				BehaviourHelperSave = BehaviourHelperSave.Clone()
			};
		}
	}
}