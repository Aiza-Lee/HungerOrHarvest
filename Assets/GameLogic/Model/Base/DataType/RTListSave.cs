using System.Collections.Generic;

namespace GameLogic {
	[System.Serializable]
	public class RTListSave<T> {
		private RTListSave() { }
		public List<Pair<string, T>> List;
		public RTListSave(List<RTPair<T>> ori) {
			List = new();
			ori.ForEach(
				(pair) => List.Add(new(pair.RepoType.ToString(), pair.Value))
			);
		}
		public RTListSave<T> Clone() {
			return new() { List = new(List) };
		}
	}
}