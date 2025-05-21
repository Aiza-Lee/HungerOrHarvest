using System;

namespace GameLogic.Model.Element.Arch
{
	[Serializable]
	public class CottageSave : ArchSaveBase {
		protected override ArchSaveBase GetDerivedClone() {
			return new CottageSave();
		}
	}
}