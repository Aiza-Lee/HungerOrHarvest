namespace GameLogic
{
	[System.Serializable]
	public class BeachLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new BeachLayerSave();
		}
	}
}