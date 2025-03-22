namespace GameLogic
{
	public class WasteLandLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.WasteLand;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new WasteLandLayerSave();
		}
	}
}