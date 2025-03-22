namespace GameLogic
{
	[System.Serializable]
	public class SnowLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new SnowLayerSave();
		}
	}
}