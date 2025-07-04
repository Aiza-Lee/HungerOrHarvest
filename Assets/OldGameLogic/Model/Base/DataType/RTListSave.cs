using System.Collections.Generic;
using System.Linq;
using OldGameLogic.Utilities;

namespace OldGameLogic {
	[System.Serializable]
	public class RTListSave<T> {
		private RTListSave() { }
		public List<Pair<EnumStringSave<RepoType>, T>> List = new();
		public RTListSave(List<RTPair<T>> other) {
			List = other
					.Select(pair => new Pair<EnumStringSave<RepoType>, T>(new(pair.RepoType), pair.Value))
					.ToList();
		}
		public RTListSave<T> Clone() {
			return new() { List = new(List) };
		}
	}
}