using UnityEngine;

namespace GameLogic.Model.Element.Vill
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