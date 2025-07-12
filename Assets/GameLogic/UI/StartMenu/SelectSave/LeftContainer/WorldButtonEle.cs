using System;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.UI.StartMenu {
	public class WorldButtonEle : MonoBehaviour, IGroupLayoutEle {
		[SerializeField] private float _eleSize;
		[SerializeField] private TextMeshProUGUI _worldNameText;
		private RectTransform _rectTrans;
		private void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		private string _worldName;

		public void SetInfo(string worldName) {
			_worldName = _worldNameText.text = worldName;
		}

		public void OnClicked() {
			UIMgr.Inst.FindPanel<SelectSavePanel>().ChooseWorld(_worldName);
		}

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set;}
		public float EleSize => _eleSize;
		public RectTransform RectTrans => _rectTrans;
		#pragma warning disable 67
		public event Action OnDirty;
		#pragma warning restore 67
		public void SetPos(float y) {
			_rectTrans.offsetMax = new(0, -y);
			_rectTrans.offsetMin = new(0, -y - _eleSize);
		}
		public void OnAddedToGroup() {
			_rectTrans.offsetMax = new(0, 0);
			_rectTrans.offsetMin = new(0, 0);
		}
		public void LogicDestroy() {
			PoolSystem.PushGO(gameObject);
		}
		#endregion
	}
}