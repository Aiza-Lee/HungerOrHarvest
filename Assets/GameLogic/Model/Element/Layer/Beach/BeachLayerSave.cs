namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class BeachLayerSave : LayerSaveBase {
		protected override LayerSaveBase GetDerivedClone() {
			return new BeachLayerSave();
		}
	}
}