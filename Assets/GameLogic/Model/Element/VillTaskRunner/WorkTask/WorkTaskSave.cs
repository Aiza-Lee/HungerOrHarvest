using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public class WorkTaskSave : TaskSaveBase {
		[HideInInspector] public ulong WorkArchId;
		protected override TaskSaveBase Clone_Derived() {
			return new WorkTaskSave() {
				WorkArchId = WorkArchId,
			};
		}
	}
}