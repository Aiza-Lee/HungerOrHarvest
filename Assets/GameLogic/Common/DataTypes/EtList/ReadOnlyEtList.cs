using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Common.DataTypes {

	[Serializable]
	public class ReadOnlyEtList<E, T> : IEnumerable<EtPair<E, T>>
	where E : Enum
	where T : struct {
		[SerializeField] private EtList<E, T> _items;
		public bool Full { get; private set; } = false;

		public ReadOnlyEtList(EtList<E, T> etList) {
			_items = etList;
			Full = etList.Full;
		}

		public IEnumerator<EtPair<E, T>> GetEnumerator() {
			foreach (var item in _items) {
				yield return item;
			}
		}

		public T this[E key] {
			get => _items[key];
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public EtList<E, T> ToNewEtList() {
			return new EtList<E, T>(_items);
		}
	}
}