using System;

namespace GameLogic
{
	[Serializable]
	public class CottageSave : ArchSaveBase {
		protected override ArchSaveBase GetDerivedClone() {
			return new CottageSave();
		}
	}
}