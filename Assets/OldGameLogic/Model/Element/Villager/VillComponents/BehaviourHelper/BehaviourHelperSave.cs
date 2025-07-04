namespace OldGameLogic.Model.Element.Vill {
	[System.Serializable]
	public class BehaviourHelperSave {
		public MyBlackBoardSave BlackBoardSave;

		public BehaviourHelperSave Clone() {
			return new BehaviourHelperSave {
				BlackBoardSave = BlackBoardSave.Clone()
			};
		}
	}
}