namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class RecoverVitTaskSave : TaskSaveBase {
		protected override TaskSaveBase Clone_Derived() {
			return new RecoverVitTaskSave() {};
		}
	}
}