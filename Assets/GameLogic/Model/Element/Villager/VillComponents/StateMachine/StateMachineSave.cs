using GameLogic.Utilities;

namespace GameLogic.Model.Element.Vill {
	[System.Serializable]
	public class StateMachineSave {
		public EnumStringSave<State> CurStaType;

		public int RecoverChance;
		public NullableSave<MoveToTargetType> MoveToTarget;
		public NullableSave<Coord> MoveTargetCoord;
		public bool IsDying;

		public StateMachineSave Clone() {
			return new StateMachineSave {
				CurStaType = CurStaType,
				RecoverChance = RecoverChance,
				MoveToTarget = MoveToTarget,
				MoveTargetCoord = MoveTargetCoord,
				IsDying = IsDying,
			};
		}
	}
}