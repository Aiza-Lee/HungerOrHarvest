namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class BuffAdderSave {
		public float Buff;
		public VitBuffType BuffType;
		public int RestTicks;
		public BuffAdderSave Clone() {
			return new BuffAdderSave() {
				Buff = Buff,
				BuffType = BuffType,
				RestTicks = RestTicks,
			};
		}
	}
}