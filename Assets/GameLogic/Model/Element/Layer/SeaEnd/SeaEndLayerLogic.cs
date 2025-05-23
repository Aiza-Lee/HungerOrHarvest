namespace GameLogic.Model.Element.Layer
{
	public class SeaEndLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.SeaEnd;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new SeaEndLayerSave();
		}
	}
}