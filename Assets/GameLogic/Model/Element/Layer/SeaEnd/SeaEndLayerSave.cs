namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class SeaEndLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new SeaEndLayerSave();
		}
	}
}