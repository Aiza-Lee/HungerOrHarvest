using System.Collections.Generic;

namespace GameLogic {
	[System.Serializable]
	public class JTListSave<T> {
		private JTListSave() {}
		public List<Pair<string, T>> List;
		public JTListSave(List<JTPair<T>> ori) {
			List = new();
			ori.ForEach(
				(pair) => List.Add(new(pair.JobType.ToString(), pair.Value))
			);
		}
		public JTListSave<T> Clone() {
			return new() { List = new(List) };
		}
	}
}