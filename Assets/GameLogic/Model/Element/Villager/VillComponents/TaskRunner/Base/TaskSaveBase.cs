using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public abstract class TaskSaveBase {
		public string TypeName;
		private TaskType? _taskType;
		public TaskType TaskType => _taskType ??= System.Enum.Parse<TaskType>(TypeName);
		[HideInInspector] public bool IsEnded;
		protected abstract TaskSaveBase Clone_Derived();
		public TaskSaveBase Clone() {
			var save = Clone_Derived();
			save.TypeName 	= TypeName;
			save.IsEnded 	= IsEnded;
			return save;
		}
	}
}