using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Common.DataTypes {
	public class EtPair<E, T>
	where E : Enum
	where T : struct {
		public E EnumType { get; set; }
		public T Value { get; set; }
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

	public class EtList<E, T> : List<EtPair<E, T>>
	where E : Enum
	where T : struct {
		private static int Elength { get; } = Enum.GetValues(typeof(E)).Length;
		public EtList() : base() { Full = false; }
		public EtList(bool fillAll = false) : base() {
			Full = fillAll;
			if (fillAll) {
				foreach (E enumType in Enum.GetValues(typeof(E))) {
					Add(new EtPair<E, T>(enumType));
				}
			}
		}
		/// <summary>
		/// 创建一个EtList，填充所有枚举类型的值为指定的value
		/// </summary>
		public EtList(T value) : base() {
			Full = true;
			foreach (E enumType in Enum.GetValues(typeof(E))) {
				Add(new EtPair<E, T>(enumType, value));
			}
		}
		public EtList(IEnumerable<EtPair<E, T>> pairs) : base() {
			base.AddRange(
				pairs.Select(pair => new EtPair<E, T>(pair))
					 .OrderBy(pair => pair.EnumType)
			);
			Full = Count == Elength;
		}

		public bool Full { get; private set; }


		public override string ToString() {
			return string.Join(", ", this);
		}

		private void EnsureIndexValid(int index) {
			if (!Full) {
				throw new InvalidOperationException("EtList is not fully initialized.");
			} else if (index < 0 || index >= Count) {
				throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
			}
		}
		public new T this[int index] {
			get {
				EnsureIndexValid(index);
				return base[index].Value;
			}
			set {
				EnsureIndexValid(index);
				base[index].Value = value;
			}
		}
	}
}