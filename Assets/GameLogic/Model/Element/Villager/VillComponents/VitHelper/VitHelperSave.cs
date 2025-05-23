using System;

namespace GameLogic.Model.Element.Vill
{
	[Serializable]
	public class VitHelperSave {
		public float Vit;

		public VitHelperSave Clone() {
			return new() {
				Vit = Vit
			};
		}
	}
}