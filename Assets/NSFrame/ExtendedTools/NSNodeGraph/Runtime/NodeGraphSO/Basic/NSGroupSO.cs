using System.Collections.Generic;
using UnityEngine;

namespace NSFrame
{
	public class NSGroupSO : ScriptableObject {
		public string Title;
		public List<NSNodeSOBase> InGroupNodes;
	}
}