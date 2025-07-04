using System;
using UnityEngine;

namespace OldGameLogic.View.UI.WorldRepoPanel
{
	public class ArchEle : MonoBehaviour, IGroupLayoutEle {

		private RectTransform _recrTrans;
		private void Awake() {
			_recrTrans = GetComponent<RectTransform>();
		}

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public float EleSize => 100f;
		public float Height => 100f;
		public RectTransform RectTrans => _recrTrans;
		#pragma warning disable 67
		public event Action OnDirty;
		#pragma warning restore 67
		public void OnAddedToGroup() {
			;
		}
		public void SetPos(float x) {
			;
		}
		public void LogicDestroy() {}
		#endregion
	}
}