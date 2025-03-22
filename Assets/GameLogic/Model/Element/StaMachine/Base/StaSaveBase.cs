using System.Collections.Generic;

namespace GameLogic
{
	[System.Serializable]
	public abstract class StaSaveBase {
		public StaType StaType;
		public ulong AttachedSMID;

		protected abstract StaSaveBase GetDerivedClone();
		public StaSaveBase Clone() {
			var save = GetDerivedClone();
				save.StaType = StaType;
			return save;
		}
	}
}