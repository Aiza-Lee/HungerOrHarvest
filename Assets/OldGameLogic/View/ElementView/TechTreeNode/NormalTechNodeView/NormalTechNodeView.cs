using System.Collections.Generic;

namespace OldGameLogic.View
{
	public class NormalTechNodeView : TechNodeViewBase {
		public List<Pair<ArchType, int>> UnlockArchLevels;
		public List<Pair<ArchType, float>> ArchBuffs;
	}
}