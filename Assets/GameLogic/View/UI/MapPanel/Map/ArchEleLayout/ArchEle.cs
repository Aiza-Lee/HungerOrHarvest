using System;
using GameLogic.View.UI.WorldVillPanel;
using UnityEngine;

namespace GameLogic.View.UI.WorldRepoPanal
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
		public event Action OnDirty;
		public void OnAddedToGroup() {
			;
		}
		public void SetPos(float x) {
			;
		}
		#endregion
	}
}