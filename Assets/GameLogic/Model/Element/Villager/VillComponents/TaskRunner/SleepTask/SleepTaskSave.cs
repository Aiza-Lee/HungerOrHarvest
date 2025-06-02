using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class SleepTaskSave : TaskSaveBase {
		protected override TaskSaveBase Clone_Derived() {
			return new SleepTaskSave();
		}
	}
}