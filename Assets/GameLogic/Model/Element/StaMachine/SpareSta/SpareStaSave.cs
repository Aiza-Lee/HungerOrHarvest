// using System.Collections.Generic;

// namespace GameLogic
// {
// 	[System.Serializable]
// 	public class SpareStaSave : StaSaveBase {
// 		public Coord Target;
// 		public bool IsArrived;
// 		public List<Coord> Route;
// 		public int Timer;
// 		public int Idx;

// 		protected override StaSaveBase GetDerivedClone() {
// 			return new SpareStaSave() {
// 				Target = Target,
// 				IsArrived = IsArrived,
// 				Route = new(Route),
// 				Timer = Timer,
// 				Idx = Idx,
// 			};
// 		}
// 	}
// }