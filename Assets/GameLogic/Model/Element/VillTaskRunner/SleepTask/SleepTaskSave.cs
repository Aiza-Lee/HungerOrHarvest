namespace GameLogic
{
	[System.Serializable]
	public class SleepTaskSave : TaskSaveBase {
		public ulong HomeID;
		protected override TaskSaveBase Clone_Derived() {
			return new SleepTaskSave() {
				HomeID = HomeID
			};
		}
	}
}