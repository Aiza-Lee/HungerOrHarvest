namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class SnowLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new SnowLayerSave();
		}
	}
}