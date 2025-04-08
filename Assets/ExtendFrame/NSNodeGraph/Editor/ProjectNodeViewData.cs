using System.Collections.Generic;
using GameLogic;
using NSFrame;

namespace ExtendFrame {
	public class ProjectNodeViewDataSO : NSNodeViewDataSO {
		public string OutputPortID;
		public string Tag, Text;
		public List<NSPair<RepoType, ulong>> Demands;
		public List<NSPair<ArchType, int>> Unlocks;
		public List<NSPair<ArchType, float>> Buffs;

		public override void GetOutputPort(NSNodeViewBase nodeView, Dictionary<string, NSPort> ports) {
			if (nodeView is ProjectNodeView projectNodeView) {
				ports.Add(OutputPortID, projectNodeView.OutputPort);
			}
		}

		public override void SetNodeView(NSNodeViewBase nodeViewBase) {
			var nodeView = nodeViewBase as ProjectNodeView;
			nodeView.TagTF.value = Tag;
			nodeView.TextTF.value = Text;
			foreach (var demand in Demands) { nodeView.AddDemand(new(demand.Key, demand.Value)); }
			foreach (var unlock in Unlocks) { nodeView.AddUnlock(new(unlock.Key, unlock.Value)); }
			foreach (var buff in Buffs) { nodeView.AddBuff(new(buff.Key, buff.Value)); }
		}
	}
}