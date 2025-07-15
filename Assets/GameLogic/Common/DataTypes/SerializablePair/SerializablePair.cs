namespace GameLogic.Common.DataTypes {
	[System.Serializable]
	public class SerializablePair<TKey, TValue> {
		public TKey Key;
		public TValue Value;

		public SerializablePair() { }

		public SerializablePair(TKey key, TValue value) {
			Key = key;
			Value = value;
		}

		public override string ToString() {
			return $"{Key}: {Value}";
		}
	}
}