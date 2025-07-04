namespace OldGameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class GrassLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.Grass;

		protected override LayerSaveBase GetDerivedClone() {
			return new GrassLayerSave();
		}
	}
}