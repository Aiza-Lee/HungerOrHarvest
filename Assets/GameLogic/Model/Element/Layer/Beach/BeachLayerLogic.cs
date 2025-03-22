namespace GameLogic
{
	public class BeachLayerLogic : LayerLogicBase {
		public override LayerType LayerType => LayerType.Beach;

		protected override void DerivedInitFromSave(LayerSaveBase save) {}
		protected override LayerSaveBase GetDerivedSave() {
			return new BeachLayerSave();
		}
	}
}