using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public abstract class TaskSaveBase {
		public TaskType TaskType;
		[HideInInspector] public bool IsEnded;
		protected abstract TaskSaveBase Clone_Derived();
		public TaskSaveBase Clone() {
			var save = Clone_Derived();
			save.TaskType = TaskType;
			save.IsEnded = IsEnded;
			return save;
		}
	}
}