namespace GameLogic.Model.Element.Layer
{
	[System.Serializable]
	public class BeachLayerSave : LayerSaveBase {
		public override LayerType LayerType => LayerType.Beach;

		protected override LayerSaveBase GetDerivedClone() {
			return new BeachLayerSave();
		}
	}
}