using System.Collections.Generic;

namespace GameLogic.Utilities
{
	public sealed class LayerComparer : IComparer<LayerLogicBase> {
		private LayerComparer() {}
		public static LayerComparer Inst { get; private set; } = new();
		public int Compare(LayerLogicBase x, LayerLogicBase y) {
			return x.LYR.CompareTo(y.LYR);
		}
	}
}