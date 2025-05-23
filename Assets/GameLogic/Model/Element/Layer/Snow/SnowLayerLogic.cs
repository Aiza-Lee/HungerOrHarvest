namespace GameLogic.Model.Element.Layer
{
	public class SnowLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.Snow;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new SnowLayerSave();
		}

	}
}