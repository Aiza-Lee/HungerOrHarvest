using System.Collections.Generic;

namespace GameLogic.View
{
	[System.Serializable]
	public class TechTreeMgrViewSave {
		public List<Pair<ulong, bool>> TechNodeStatus;
	}
}