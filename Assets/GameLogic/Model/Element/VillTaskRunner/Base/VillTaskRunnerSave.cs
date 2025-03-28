using System.Collections.Generic;

namespace GameLogic
{
	[System.Serializable]
	public class VillTaskRunnerSave {
		[UnityEngine.SerializeReference] public List<TaskSaveBase> Tasks = new();
		public VillTaskRunnerSave Clone() {
			return new VillTaskRunnerSave() {
				Tasks = new(Tasks),
			};
		}
	}
}