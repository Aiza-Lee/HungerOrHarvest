using NsEcsFrame.Core;

namespace GameLogic.Common.Components {
	public struct ExpComponent : IComponent {
		public int Exp;
		public int Level;
		public ExpComponent(int exp, int level) {
			Exp = exp;
			Level = level;
		}
	}
}