namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class VitBuffAdderSave {
		public float Buff;
		public VitBuffType BuffType;
		public int RestTicks;
		public VitBuffAdderSave Clone() {
			return new VitBuffAdderSave() {
				Buff = Buff,
				BuffType = BuffType,
				RestTicks = RestTicks,
			};
		}
	}
}