using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Common.Logic {
	public class OLComponent : IComponent {
		public OL OL;
		public bool IsDirty = true;
	}
}