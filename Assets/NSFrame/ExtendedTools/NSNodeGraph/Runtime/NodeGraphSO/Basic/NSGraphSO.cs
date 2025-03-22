using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public class NSGraphSO : ScriptableObject {
		public List<NSNodeSOBase> UngroupedNodes;
		public List<NSGroupSO> Groups;
	}
}
