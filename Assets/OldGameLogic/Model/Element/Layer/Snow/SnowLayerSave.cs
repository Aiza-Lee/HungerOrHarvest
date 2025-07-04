namespace OldGameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class SnowLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.Snow;

		protected override LayerSaveBase GetDerivedClone() {
			return new SnowLayerSave();
		}
	}
}