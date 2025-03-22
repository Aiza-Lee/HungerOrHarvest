namespace GameLogic
{
	public abstract class LayerLogicBase : ISaveable<LayerSaveBase> {
		private ulong _id;
		private int _lyr;
		public ulong ID => _id;
		public int LYR => _lyr;

		public abstract LayerType LayerType { get; }


		protected abstract LayerSaveBase GetDerivedSave();
		public LayerSaveBase GetSave() {
			var save = GetDerivedSave();
				save.ID = _id;
				save.LYR = _lyr;
				save.LayerType = LayerType;
			return save;
		}

		protected abstract void DerivedInitFromSave(LayerSaveBase save);
		public virtual void InitFromSave(LayerSaveBase save) {
			DerivedInitFromSave(save);
			_id = save.ID;
			_lyr = save.LYR;
		}
	}
}