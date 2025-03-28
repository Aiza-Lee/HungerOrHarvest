using System.Collections.Generic;

namespace GameLogic
{
	[System.Serializable]
	public class MoveToTaskSave : TaskSaveBase {
		public Coord Target;
		public List<Coord> Route;
		public int Timer;
		public int Idx;
		protected override TaskSaveBase Clone_Derived() {
			return new MoveToTaskSave() {
				Target = Target,
				Route = new(Route),
				Timer = Timer,
				Idx = Idx,
			};
		}
	}
}