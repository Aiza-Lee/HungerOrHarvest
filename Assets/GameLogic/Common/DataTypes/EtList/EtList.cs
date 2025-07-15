using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[Serializable]
	public class EtPair<E, T>
	where E : Enum
	where T : struct {
		[JsonProperty] public E EnumType;
		[JsonProperty] public T Value;
		public int Index => Convert.ToInt32(EnumType);

		public EtPair() : this(default, default) { }
		public EtPair(E enumType, T value) {
			EnumType = enumType;
			Value = value;
		}
		public EtPair(E enumType) : this(enumType, default) { }
		public EtPair(EtPair<E, T> other) {
			EnumType = other.EnumType;
			Value = other.Value;
		}

		public override string ToString() => $"{EnumType}: {Value}";
	}

	[Serializable]
	public class EtList<E, T> : IEnumerable<EtPair<E, T>>
	where E : Enum
	where T : struct {
		private static int Elength { get; } = Enum.GetValues(typeof(E)).Length;
		public EtList() : base() { Full = false; }
		public EtList(bool fillAll = false) : base() {
			Full = fillAll;
			if (fillAll) {
				foreach (E enumType in Enum.GetValues(typeof(E))) {
					Items.Add(new EtPair<E, T>(enumType));
				}
				SortSelf();
			}
		}
		/// <summary>
		/// 创建一个EtList，填充所有枚举类型的值为指定的value
		/// </summary>
		public EtList(T value) : base() {
			foreach (E enumType in Enum.GetValues(typeof(E))) {
				Items.Add(new EtPair<E, T>(enumType, value));
			}
			SortSelf();
			Full = true;
		}
		public EtList(IEnumerable<EtPair<E, T>> pairs) : base() {
			Items.AddRange(
				pairs.Select(pair => new EtPair<E, T>(pair))
					 .OrderBy(pair => pair.EnumType)
			);
			if (Items.Count == Elength) {
				SortSelf();
				Full = Items.Count == Elength;
			}
		}

		/// <summary>
		/// 检查EtList是否已填充所有枚举类型的值为default
		/// </summary>
		public bool IsDefault() {
			if (!Full) return false;
			for (int i = 0; i < Items.Count; i++) {
				if (!EqualityComparer<T>.Default.Equals(Items[i].Value, default)) {
					return false;
				}
			}
			return true;
		}

		[JsonProperty] public List<EtPair<E, T>> Items = new();
		[JsonProperty] public bool Full { get; private set; }

		public override string ToString() {
			return string.Join(", ", this);
		}

		private void EnsureIndexValid(int index) {
			if (!Full) {
				throw new InvalidOperationException("EtList is not fully initialized.");
			} else if (index < 0 || index >= Items.Count) {
				throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
			}
		}

		public IEnumerator<EtPair<E, T>> GetEnumerator() {
			return Items.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public T this[int index] {
			get {
				EnsureIndexValid(index);
				return Items[index].Value;
			}
			set {
				EnsureIndexValid(index);
				Items[index].Value = value;
			}
		}
		public T this[E enumType] {
			get => this[enumType.ToListIndex()];
			set => this[enumType.ToListIndex()] = value;
		}
		public void ForEach(Action<EtPair<E, T>> action) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			for (int i = 0; i < Items.Count; i++) {
				action(Items[i]);
			}
		}
		public void SortSelf() {
			Items.Sort((a, b) => a.EnumType.CompareTo(b.EnumType));
		}
		public void Fill(T value) {
			for (int i = 0; i < Items.Count; i++) {
				Items[i].Value = value;
			}
		}
	}
}