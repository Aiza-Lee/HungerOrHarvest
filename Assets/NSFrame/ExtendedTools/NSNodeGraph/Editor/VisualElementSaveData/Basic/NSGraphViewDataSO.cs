using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public class NSGraphViewDataSO : ScriptableObject {
		public List<NSNodeViewDataSO> UngroupedNodeViewDatas;
		public List<NSGroupViewDataSO> GroupViewDatas;
		/// <summary>
		/// 存储 OutputPortID -> InputPortID
		/// </summary>
		public List<NSPair<string, string>> Edges;
	}
}