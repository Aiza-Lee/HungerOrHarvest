using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public class VillTaskRunnerSave {
		[HideInInspector][SerializeReference] public List<TaskSaveBase> Tasks = new();
		public VillTaskRunnerSave Clone() {
			return new VillTaskRunnerSave() {
				Tasks = new(Tasks),
			};
		}
	}
}