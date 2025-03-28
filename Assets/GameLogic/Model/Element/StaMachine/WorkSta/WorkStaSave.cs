// using System.Collections.Generic;

// namespace GameLogic 
// {
// 	[System.Serializable]
// 	public class WorkStaSave : StaSaveBase {
// 		public bool IsArrived;
// 		public List<Coord> Route;
// 		public int Timer;
// 		public int Idx;
// 		public ulong TargetArchID;
// 		protected override StaSaveBase GetDerivedClone() {
// 			return new WorkStaSave() {
// 				IsArrived = IsArrived,
// 				Route = (Route == null ? null : new(Route)),
// 				Timer = Timer,
// 				Idx = Idx,
// 			};
// 		}
// 	}
// }