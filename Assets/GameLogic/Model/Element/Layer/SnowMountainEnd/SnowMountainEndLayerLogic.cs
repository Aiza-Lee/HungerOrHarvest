namespace GameLogic
{
	public class SnowMountainEndLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.SnowMountainEnd;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new SnowMountainEndLayerSave();
		}
	}
}