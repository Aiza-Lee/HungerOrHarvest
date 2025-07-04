namespace OldGameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class WasteLandLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.WasteLand;

		protected override LayerSaveBase GetDerivedClone() {
			return new WasteLandLayerSave();
		}
	}
}