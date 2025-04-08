using System.Collections.Generic;

namespace GameLogic.View
{
	public class NormalTechNodeView : TechNodeViewBase {
		public List<Pair<ArchType, int>> UnlockArchLevels;
		public List<Pair<ArchType, float>> ArchBuffs;
	}
}