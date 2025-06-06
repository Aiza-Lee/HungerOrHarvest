namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class SeaEndLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.SeaEnd;

		protected override LayerSaveBase GetDerivedClone() {
			return new SeaEndLayerSave();
		}
	}
}