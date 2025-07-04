namespace OldGameLogic.Utilities {
	[System.Serializable]
	public class NullableSave<T> where T : struct {
		public bool HasValue;
		public T Value;
		public NullableSave() {
			HasValue = false;
		}
		public NullableSave(T? value) {
			if (value.HasValue) {
				HasValue = true;
				Value = value.Value;
			} else {
				HasValue = false;
			}
		}
		public T? ToNullable() {
			if (HasValue) {
				return Value;
			}
			return null;
		}
	}
}