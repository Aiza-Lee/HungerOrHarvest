using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	public class NSGroupViewDataSO : ScriptableObject {
		public string Title;
		public Vector2 Position;
		public List<NSNodeViewDataSO> InGroupNodeViewDatas;
	}
}