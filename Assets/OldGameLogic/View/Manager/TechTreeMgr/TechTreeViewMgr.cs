using System.Collections.Generic;
using OldGameLogic.Utilities;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.View {
	public sealed class TechTreeViewMgr : MonoSingleton<TechTreeViewMgr>, IMananger, ISaveable<TechTreeMgrViewSave> {

		private readonly Dictionary<ulong, TechNodeViewBase> _nodeViews = new();

		protected override void Awake() {
			base.Awake();
			_nodeViews.Clear();
			var cnt = transform.childCount;
			for (int i = 0; i < cnt; ++i) {
				var nodeView = transform.GetChild(i).GetComponent<TechNodeViewBase>();
				_nodeViews.Add(nodeView.NodeID, nodeView);
			}
		}


		#region PublicMethods

		public bool UnlockNode(ulong nodeID) {
			if (_nodeViews.TryGetValue(nodeID, out var nodeView)) {
				nodeView.Unlocked = true;
				return true;
			} else {
				Debug.LogWarning($"TechNodeViewBase with ID {nodeID} not found!");
				return false;
			}
		}

		#endregion


		#region IClearMgr
		public void ClearMgr() { }
		#endregion

		#region ISaveable
		public TechTreeMgrViewSave GetSave() {
			var sv = new TechTreeMgrViewSave() {
				TechNodeStatus = new()
			};
			foreach (var nv in _nodeViews.Values) {
				sv.TechNodeStatus.Add(new(nv.NodeID, nv.Unlocked));
			}
			return sv;
		}

		public void InitFromSave(TechTreeMgrViewSave save) {
			foreach (var pair in save.TechNodeStatus) {
				if (_nodeViews.TryGetValue(pair.Key, out var nv)) {
					nv.Unlocked = pair.Value;
				} else {
					Debug.LogError($"TechNodeViewBase with ID {pair.Key} not found!");
				}
			}
		}
		#endregion

	}
}