using System.Collections.Generic;
using GameLogic.Utilities;
using UnityEngine;

namespace GameLogic.View {
	/// <summary>
	/// 这里为了方便在unity中编辑科技树，就把model的逻辑和view混在一起了
	/// </summary>
	public abstract class TechNodeViewBase : MonoBehaviour, ISaveable<TechNodeViewSave> {
		public ulong NodeID;
		public bool Unlocked;
		public string Title;
		[TextArea(3, 10)] public string Description;

		[SerializeReference] public List<TechNodeViewBase> NextNodes;

		#region ISaveable
		public TechNodeViewSave GetSave() {
			var sv = new TechNodeViewSave {
				NodeID = NodeID,
				Unlocked = Unlocked
			};
			return sv;
		}
		public void InitFromSave(TechNodeViewSave save) {
			NodeID = save.NodeID;
			Unlocked = save.Unlocked;
		}
		#endregion
	}
}