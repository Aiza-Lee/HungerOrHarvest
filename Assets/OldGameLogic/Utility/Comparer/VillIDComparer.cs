using System.Collections.Generic;
using OldGameLogic.Model.Element.Vill;

namespace OldGameLogic.Utilities
{
	public sealed class VillIDComparer : IComparer<VillLogicBase> {
		private VillIDComparer() {}
		public static VillIDComparer Inst { get; } = new();

		public int Compare(VillLogicBase x, VillLogicBase y) {
			return x.ID.CompareTo(y.ID);
		}
	}
}