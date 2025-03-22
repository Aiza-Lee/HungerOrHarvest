namespace GameLogic
{
	[System.Serializable]
	public class SeaEndLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new SeaEndLayerSave();
		}
	}
}