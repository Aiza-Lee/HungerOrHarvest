namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class WasteLandLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new WasteLandLayerSave();
		}
	}
}