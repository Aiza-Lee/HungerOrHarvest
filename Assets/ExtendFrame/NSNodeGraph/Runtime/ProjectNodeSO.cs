using System.Collections.Generic;
using NSFrame;
using UnityEngine;
using GameLogic;

namespace ExtendFrame {
	public class ProjectNodeSO : NSNodeSOBase {
		public List<NSNodeSOBase> NextNodes;
		public string Tag;
		[TextArea(5, 30)] public string Text;
		public List<NSPair<RepoType, ulong>> Demands;
		public List<NSPair<ArchType, int>> Unlocks;
		public List<NSPair<ArchType, float>> Buffs;

	}
}