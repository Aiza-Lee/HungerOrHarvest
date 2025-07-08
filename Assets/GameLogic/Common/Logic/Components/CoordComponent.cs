using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Common.Logic {
	public class CoordComponent : IComponent {
		public Coord Coord;
		public bool IsDirty;
	}
}