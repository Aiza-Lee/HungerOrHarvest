using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class TaskRunnerSave {
		[HideInInspector][SerializeReference] public List<TaskSaveBase> Tasks = new();
		public TaskRunnerSave Clone() {
			return new TaskRunnerSave() {
				Tasks = new(Tasks),
			};
		}
	}
}