namespace GameLogic
{
	[System.Serializable]
	public class GrassLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new GrassLayerSave();
		}
	}
}