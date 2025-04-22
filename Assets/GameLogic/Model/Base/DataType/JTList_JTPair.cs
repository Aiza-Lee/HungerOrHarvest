using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{

	[System.Serializable] 
	public class JTPair<T> {
		public JobType Job;
		public T Value;
		public int Index => (int) Job;
		public JTPair(JobType jobType, T value) {
			Job = jobType;
			Value = value;
		}
		public JTPair<T> Clone() {
			return new(Job, Value);
		}
	}

	[System.Serializable]
	public class JTList<T> {
		public List<JTPair<T>> List;
		[HideInInspector] public bool Full;
		public int Count => List.Count;

		public JTList(bool fill = false) {
			List = new();
			if (fill) {
				for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
					List.Add(new((JobType)i, default));
				}
			}
		}
		public JTList() {
			Full = false;
			List = new();
		}
		public JTList<T> Clone() {
			var nw = new JTList<T> { Full = this.Full };
			List.ForEach( (pair) => nw.List.Add(pair) );
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
				List.Add(new((JobType)i, default));
			}
			if (ori != null) foreach (var pair in ori) {
				List[pair.Index].Value = pair.Value;
			}
			return this;
		}
	}
}