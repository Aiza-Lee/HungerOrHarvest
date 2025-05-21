using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class SleepTaskSave : TaskSaveBase {
		[HideInInspector] public ulong HomeID;
		protected override TaskSaveBase Clone_Derived() {
			return new SleepTaskSave() {
				HomeID = HomeID
			};
		}
	}
}