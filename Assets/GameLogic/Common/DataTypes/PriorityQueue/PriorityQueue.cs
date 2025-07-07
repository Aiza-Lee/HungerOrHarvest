using System;
using System.Collections.Generic;

namespace GameLogic.Common.DataTypes {
	public class PriorityQueue<T> {
		public struct Node {
			public T Value;
			public int Key;
		}

		private readonly List<Node> _heap;
		private readonly IComparer<int> _cmp;

		public PriorityQueue() : this(Comparer<int>.Default) { }

		public PriorityQueue(IComparer<int> cmp) {
			_cmp = cmp ?? Comparer<int>.Default;
			_heap = new();
		}

		public int Count => _heap.Count;

		public void Enqueue(T value, int key) {
			_heap.Add(new Node { Value = value, Key = key });
			Swim(_heap.Count - 1);
		}

		public Node Dequeue() {
			if (_heap.Count == 0)
				throw new InvalidOperationException("Queue is empty.");

			var top = _heap[0];
			_heap[0] = _heap[^1];
			_heap.RemoveAt(_heap.Count - 1);

			if (_heap.Count > 0)
				Sink(0);

			return top;
		}

		public Node Peek() {
			if (_heap.Count == 0)
				throw new InvalidOperationException("Queue is empty.");
			return _heap[0];
		}

		private void Swim(int index) {
			while (index > 0) {
				int parentIndex = (index - 1) / 2;
				if (_cmp.Compare(_heap[parentIndex].Key, _heap[index].Key) <= 0)
					break;

				Swap(parentIndex, index);
				index = parentIndex;
			}
		}

		private void Sink(int idx) {
			int lsIdx;
			while ((lsIdx = 2 * idx + 1) < _heap.Count) {
				int minChildIndex = lsIdx;
				int rsIdx = lsIdx + 1;

				if (rsIdx < _heap.Count &&
					_cmp.Compare(_heap[rsIdx].Key, _heap[lsIdx].Key) < 0) {
					minChildIndex = rsIdx;
				}

				if (_cmp.Compare(_heap[idx].Key, _heap[minChildIndex].Key) <= 0)
					break;

				Swap(idx, minChildIndex);
				idx = minChildIndex;
			}
		}

		private void Swap(int i, int j) {
			(_heap[i], _heap[j]) = (_heap[j], _heap[i]);
		}
	}
}