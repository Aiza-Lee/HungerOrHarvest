namespace GameLogic
{
	public class GrassLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.Grass;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new GrassLayerSave();
		}
	}
}