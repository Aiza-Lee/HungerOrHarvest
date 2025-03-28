namespace GameLogic
{
	[System.Serializable]
	public abstract class TaskSaveBase {
		public TaskType TaskType;
		public bool IsEnded;
		protected abstract TaskSaveBase Clone_Derived();
		public TaskSaveBase Clone() {
			var save = Clone_Derived();
			save.TaskType = TaskType;
			save.IsEnded = IsEnded;
			return save;
		}
	}
}