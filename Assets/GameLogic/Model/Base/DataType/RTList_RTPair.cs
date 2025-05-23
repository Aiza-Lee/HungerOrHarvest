using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;
using UnityEngine;

namespace GameLogic
{
	public class RTPair<T> {
		public RepoType RepoType;
		public T Value;
		public int Index => (int) RepoType;
		public RTPair(RepoType type, T value) {
			RepoType = type;
			Value = value;
		}
		public RTPair(RTPair<T> other) {
			RepoType = other.RepoType;
			Value = other.Value;
		}
	}

	// todo: 由于相当频繁的创建和销毁，考虑使用对象池优化，注意手动回收
	public class RTList<T> : ISaveable<RTListSave<T>> {
		public List<RTPair<T>> List;
		/// <summary>
		/// 将每一个值设为默认值(0)
		/// </summary>
		public void Clear() {
			List.ForEach((pair) => pair.Value = default);
		}
		public int Count => List.Count;
		[HideInInspector] public bool Full;


		public RTList(bool fill = false) {
			List = new();
			if (fill) {
				Full = true;
				for (int i = 0; i < ConstMgr.REPO_TYPE_SIZE; ++i)
					List.Add(new((RepoType) i, default));
			}
		}
		public RTList() {
			Full = false;
			List = new();
		}

		public RTList<T> Clone() {
			var nw = new RTList<T> {
				Full = this.Full
			};
			List.ForEach((pair) => nw.List.Add(new(pair)));
			return nw;
		}

		public RTPair<T> this[int index] {
			get {
				if (!Full) {
					Debug.LogWarning("Donnot use index when list is not full.");
				}
				return List[index];
			}
			set => List[index] = value;
		}

		public RTList<T> ConvertToFull() {
			if (Full) { return this; }
			Full = true;
			var ori = List;
			List = new();
			for (int i = 0; i < ConstMgr.REPO_TYPE_SIZE; ++i) {
				List.Add(new((RepoType) i, default));
			}
			if (ori != null) foreach (var pair in ori) {
					List[pair.Index].Value = pair.Value;
				}
			return this;
		}

		#region ISaveable
		public RTListSave<T> GetSave() {
			return new(List);
		}
		public void InitFromSave(RTListSave<T> save) {
			List.Clear();
			if (save == null) {
				Full = false;
				return;
			}
			save.List.ForEach(
				(pair) => List.Add(new(Enum.Parse<RepoType>(pair.Key), pair.Value))
			);
			List.Sort((a, b) => a.Index - b.Index);
			Full = List.Count == ConstMgr.REPO_TYPE_SIZE;
		}
		public void InitFromSave_Full(RTListSave<T> save) {
			InitFromSave(save);
			ConvertToFull();
		}
		#endregion
	}
}