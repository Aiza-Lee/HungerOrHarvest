using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

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
					_items.Add(new EtPair<E, T>(enumType));
				}
				SortSelf();
			}
		}
		/// <summary>
		/// 创建一个EtList，填充所有枚举类型的值为指定的value
		/// </summary>
		public EtList(T value) : base() {
			foreach (E enumType in Enum.GetValues(typeof(E))) {
				_items.Add(new EtPair<E, T>(enumType, value));
			}
			SortSelf();
			Full = true;
		}
		public EtList(IEnumerable<EtPair<E, T>> pairs) : base() {
			_items.AddRange(
				pairs.Select(pair => new EtPair<E, T>(pair))
					 .OrderBy(pair => pair.EnumType)
			);
			SortSelf();
			Full = _items.Count == Elength;
		}

		[JsonProperty][SerializeField] private List<EtPair<E, T>> _items = new();
		[JsonProperty] public bool Full { get; private set; }
		
		public override string ToString() {
			return string.Join(", ", this);
		}

		private void EnsureIndexValid(int index) {
			if (!Full) {
				throw new InvalidOperationException("EtList is not fully initialized.");
			} else if (index < 0 || index >= _items.Count) {
				throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
			}
		}

		public IEnumerator<EtPair<E, T>> GetEnumerator() {
			return _items.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public T this[int index] {
			get {
				EnsureIndexValid(index);
				return _items[index].Value;
			}
			set {
				EnsureIndexValid(index);
				_items[index].Value = value;
			}
		}
		public void ForEach(Action<EtPair<E, T>> action) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			for (int i = 0; i < _items.Count; i++) {
				action(_items[i]);
			}
		}
		public void SortSelf() {
			_items.Sort((a, b) => a.EnumType.CompareTo(b.EnumType));
		}

	}
}