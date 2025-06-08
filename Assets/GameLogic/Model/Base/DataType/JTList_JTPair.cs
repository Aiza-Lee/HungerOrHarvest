using System.Collections.Generic;
using GameLogic.Model.Mgr;
using GameLogic.Utilities;
using UnityEngine;

namespace GameLogic {
	/// <summary>
	/// 深拷贝不能实现 T 类型的深拷贝，需要注意
	/// </summary>
	public class JTPair<T> {
		public JobType JobType;
		public T Value;
		public int Index => (int) JobType;
		public JTPair(JobType jobType, T value) {
			JobType = jobType;
			Value = value;
		}
		public JTPair<T> Clone() {
			return new(JobType, Value);
		}
	}

	/// <summary>
	/// 深拷贝不能实现 T 类型的深拷贝，需要注意
	/// </summary>
	public class JTList<T> : ISaveable<JTListSave<T>> {
		public List<JTPair<T>> List;
		[HideInInspector] public bool Full;
		public int Count => List.Count;

		/// <summary>
		/// 将每一个值设为默认值(0)
		/// </summary>
		public void Clear() {
			List.ForEach((pair) => pair.Value = default);
		}

		/// <summary>
		/// 给定部分值创造一个JTList
		/// </summary>
		public JTList(params JTPair<T>[] pairs) {
			List = new List<JTPair<T>>(pairs);
			List.Sort((a, b) => a.Index - b.Index);
			Full = List.Count == ConstMgr.JOB_TYPE_SIZE;
		}

		/// <summary>
		/// 创造一个每一个value都是给定值的Full List
		/// </summary>
		public JTList(T val) {
			Full = true;
			List = new();
			for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
				List.Add(new((JobType) i, val));
			}
		}

		public JTList(bool fill = false) {
			List = new();
			if (fill) {
				for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
					List.Add(new((JobType) i, default));
				}
			}
		}
		public JTList() {
			Full = false;
			List = new();
		}
		public JTList<T> Clone() {
			var nw = new JTList<T> { Full = this.Full };
			List.ForEach((pair) => nw.List.Add(pair.Clone()));
			return nw;
		}
		public JTPair<T> this[int index] {
			get {
				if (!Full) { Debug.LogWarning("Donnot use index when list is not full."); }
				return List[index];
			}
			set => List[index] = value;
		}
		public JTList<T> ConvertToFull() {
			if (Full) { return this; }
			Full = true;
			var ori = List;
			List = new();
			for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
				List.Add(new((JobType) i, default));
			}
			if (ori != null) foreach (var pair in ori) {
					List[pair.Index].Value = pair.Value;
				}
			return this;
		}

		#region ISaveable
		public JTListSave<T> GetSave() {
			return new(List);
		}

		public void InitFromSave(JTListSave<T> save) {
			List.Clear();
			if (save == null) {
				Full = false;
				return;
			}
			save.List.ForEach(
				(pair) => List.Add(new(pair.Key.ToEnum().Value, pair.Value))
			);
			List.Sort((a, b) => a.Index - b.Index);
			Full = List.Count == ConstMgr.JOB_TYPE_SIZE;
		}
		public void InitFromSave_Full(JTListSave<T> save) {
			InitFromSave(save);
			ConvertToFull();
		}
		#endregion
	}
}