using System.Collections.Generic;
using System.Linq;
using OldGameLogic.Utilities;

namespace OldGameLogic {
	[System.Serializable]
	public class JTListSave<T> {
		private JTListSave() {}
		public List<Pair<EnumStringSave<JobType>, T>> List;
		public JTListSave(List<JTPair<T>> ori) {
			List = ori.Select(
				pair => new Pair<EnumStringSave<JobType>, T>(new(pair.JobType), pair.Value)
			).ToList();
		}
		public JTListSave<T> Clone() {
			return new() { List = new(List) };
		}
	}
}