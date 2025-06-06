namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class SnowMountainEndLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.SnowMountainEnd;

		protected override LayerSaveBase GetDerivedClone() {
			return new SnowMountainEndLayerSave();
		}
	}
}