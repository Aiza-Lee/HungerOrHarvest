using System.Collections.Generic;

namespace OldGameLogic.View
{
	[System.Serializable]
	public class TechTreeMgrViewSave {
		public List<Pair<ulong, bool>> TechNodeStatus;
	}
}