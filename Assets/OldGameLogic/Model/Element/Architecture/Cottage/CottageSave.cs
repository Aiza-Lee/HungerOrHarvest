using System;

namespace OldGameLogic.Model.Element.Arch
{
	[Serializable]
	public class CottageSave : ArchSaveBase {
		public override ArchType ArchType => ArchType.Cottage;
		
		protected override ArchSaveBase GetDerivedClone() {
			return new CottageSave();
		}
	}
}