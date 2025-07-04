namespace OldGameLogic
{
	[System.Serializable]
	public class Pair<K, V> {
		public K Key;
		public V Value;
		public Pair(K key, V value) {
			Key = key;
			Value = value;
		}
		public Pair<K, V> Clone() {
			return new(Key, Value);
		}
	}
}