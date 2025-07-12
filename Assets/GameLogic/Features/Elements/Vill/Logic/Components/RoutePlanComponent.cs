using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// RoutePlanComponent 用于存储村民的移动路线和当前移动索引。
	/// </summary>
	[System.Serializable]
	public class RoutePlanComponent : IComponent {
		public List<Coord> MoveRoute = new();
		public int CurMoveIndex;
	}
}