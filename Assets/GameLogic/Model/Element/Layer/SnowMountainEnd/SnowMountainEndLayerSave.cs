namespace GameLogic
{
	[System.Serializable]
	public class SnowMountainEndLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new SnowMountainEndLayerSave();
		}
	}
}