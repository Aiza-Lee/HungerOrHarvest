using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class WorkTaskSave : TaskSaveBase {
		protected override TaskSaveBase Clone_Derived() {
			return new WorkTaskSave();
		}
	}
}