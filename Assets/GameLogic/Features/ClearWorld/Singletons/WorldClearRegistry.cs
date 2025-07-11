using System.Collections.Generic;

namespace GameLogic.Features.ClearWorld {
	public sealed class WorldClearRegistry : IWorldClearRespondable {
		private WorldClearRegistry() {}
		public static WorldClearRegistry Inst { get; } = new();

		private readonly List<IWorldClearRespondable> _respondables = new();
		public void Register(IWorldClearRespondable respondable) {
			if (!_respondables.Contains(respondable)) {
				_respondables.Add(respondable);
			}
		}
		public void Unregister(IWorldClearRespondable respondable) {
			if (_respondables.Contains(respondable)) {
				_respondables.Remove(respondable);
			}
		}

		public void RespondWorldClear() {
			foreach (var respondable in _respondables) {
				respondable.RespondWorldClear();
			}
		}
	}
}