using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	public class TextNodeSO : NSNodeSOBase {
		public List<NSNodeSOBase> NextNodes;
		[TextArea(5, 30)] public string Text;
	}
}