namespace OldGameLogic.Utilities {
	[System.Serializable]
	public class EnumStringSave<T> where T : struct, System.Enum {
		public string Value;

		public EnumStringSave() {
			Value = string.Empty;
		}
		public EnumStringSave(T value) {
			Value = value.ToString();
		}

		public bool IsValid() {
			return !string.IsNullOrEmpty(Value) && System.Enum.IsDefined(typeof(T), Value);
		}

		public T? ToEnum() {
			if (System.Enum.TryParse<T>(Value, out var result)) {
				return result;
			} else {
				return null;
			}
		}

		public override string ToString() {
			return Value;
		}
	}
}