namespace GameLogic.Model.Element.Layer
{
	public class SnowMountainEndLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.SnowMountainEnd;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new SnowMountainEndLayerSave();
		}
	}
}