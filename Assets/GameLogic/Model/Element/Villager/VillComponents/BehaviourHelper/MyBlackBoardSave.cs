using System.Collections.Generic;

namespace GameLogic.Model.Element.Vill {
	[System.Serializable]
	public class MyBlackBoardSave {
		public List<Coord> MoveRoute;
		public int CurMoveIndex;
		public ulong LastMoveTime;

		public int RecoverChance;
		public bool RecoverMode;

		public bool IsDying;
		public bool Die;

		#region LastTickInfo
		public ulong LastTickHomeID;
		public ulong LastTickBondedWorkArchID;
		public bool LastTickInDay;
		#endregion

		public MyBlackBoardSave Clone() {
			return new MyBlackBoardSave {
				MoveRoute = new List<Coord>(MoveRoute),
				CurMoveIndex = CurMoveIndex,
				LastMoveTime = LastMoveTime,
				RecoverChance = RecoverChance,
				RecoverMode = RecoverMode,
				IsDying = IsDying,
				Die = Die,
				LastTickHomeID = LastTickHomeID,
				LastTickBondedWorkArchID = LastTickBondedWorkArchID,
				LastTickInDay = LastTickInDay
			};
		}
	}
}