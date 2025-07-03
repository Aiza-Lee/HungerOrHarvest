using System.Collections;
using System.Collections.Generic;

namespace NsEcsFrame.Utils {
    /// <summary>
    /// 分页稀疏集（SparseSet），sparse为PageList实现，每页32个元素，适合稀疏ID场景。
    /// </summary>
    public class SparseSet<T> : IEnumerable<T> where T : class {
        private const int PageSize = 32;
        private readonly List<int[]> _sparsePages = new(); // 每页32个int
        private readonly List<int> _denseIds = new();
        private readonly List<T> _dense = new();
        private int _count;

        public int Count => _count;

        public void Add(uint id, T value) {
            int intId = (int)id;
            int pageIdx = intId / PageSize;
            int offset = intId % PageSize;
            EnsureSparsePage(pageIdx);
            int[] page = _sparsePages[pageIdx];
            if (Contains(id)) return;
            page[offset] = _dense.Count + 1; // +1 表示有效，0为未用
            _denseIds.Add(intId);
            _dense.Add(value);
            _count++;
        }

        public bool Remove(uint id) {
            int intId = (int)id;
            int pageIdx = intId / PageSize;
            int offset = intId % PageSize;
            if (pageIdx >= _sparsePages.Count) return false;
            int[] page = _sparsePages[pageIdx];
            int denseIndex = page[offset] - 1;
            if (denseIndex < 0 || denseIndex >= _count || _denseIds[denseIndex] != intId) return false;
            int last = _count - 1;
            if (denseIndex != last) {
                int lastId = _denseIds[last];
                _dense[denseIndex] = _dense[last];
                _denseIds[denseIndex] = lastId;
                int lastPageIdx = lastId / PageSize;
                int lastOffset = lastId % PageSize;
                _sparsePages[lastPageIdx][lastOffset] = denseIndex + 1;
            }
            _dense.RemoveAt(last);
            _denseIds.RemoveAt(last);
            page[offset] = 0;
            _count--;
            return true;
        }

        public bool Contains(uint id) {
            int intId = (int)id;
            int pageIdx = intId / PageSize;
            int offset = intId % PageSize;
            if (pageIdx >= _sparsePages.Count) return false;
            int[] page = _sparsePages[pageIdx];
            int denseIndex = page[offset] - 1;
            return denseIndex >= 0 && denseIndex < _count && _denseIds[denseIndex] == intId;
        }

        public T Get(uint id) {
            int intId = (int)id;
            int pageIdx = intId / PageSize;
            int offset = intId % PageSize;
            if (pageIdx >= _sparsePages.Count) return null;
            int[] page = _sparsePages[pageIdx];
            int denseIndex = page[offset] - 1;
            if (denseIndex < 0 || denseIndex >= _count || _denseIds[denseIndex] != intId) return null;
            return _dense[denseIndex];
        }

        /// <summary>
        /// 清空所有内容，重置稀疏集
        /// </summary>
        public void Clear() {
            for (int i = 0; i < _sparsePages.Count; i++) {
                var page = _sparsePages[i];
                for (int j = 0; j < PageSize; j++) page[j] = 0;
            }
            _denseIds.Clear();
            _dense.Clear();
            _count = 0;
        }

        /// <summary>
        /// 安全获取指定ID的元素，返回是否存在
        /// </summary>
        public bool TryGetValue(uint id, out T value) {
            value = null;
            int intId = (int)id;
            int pageIdx = intId / PageSize;
            int offset = intId % PageSize;
            if (pageIdx >= _sparsePages.Count) return false;
            int[] page = _sparsePages[pageIdx];
            int denseIndex = page[offset] - 1;
            if (denseIndex < 0 || denseIndex >= _count || _denseIds[denseIndex] != intId) return false;
            value = _dense[denseIndex];
            return true;
        }

        public IEnumerable<uint> Ids {
            get {
                for (int i = 0; i < _count; i++) {
                    yield return (uint)_denseIds[i];
                }
            }
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < _count; i++) {
                yield return _dense[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void EnsureSparsePage(int pageIdx) {
            while (_sparsePages.Count <= pageIdx) {
                var page = new int[PageSize]; // 默认0，0表示未用
                _sparsePages.Add(page);
            }
        }
    }
}
