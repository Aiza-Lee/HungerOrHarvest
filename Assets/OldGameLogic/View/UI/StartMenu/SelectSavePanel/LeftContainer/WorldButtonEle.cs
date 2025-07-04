using System;
using NSFrame;
using TMPro;
using UnityEngine;

namespace OldGameLogic.View.UI.StartMenu.SelectSavePanel
{
	public class WorldButtonEle : MonoBehaviour, IGroupLayoutEle {
		[SerializeField] private float _eleSize;
		[SerializeField] private TextMeshProUGUI _worldNameText;
		private RectTransform _rectTrans;
		private void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		private string _worldHash;

		public void SetInfo(string worldHash, string worldName) {
			_worldHash = worldHash;
			_worldNameText.text = worldName;
		}

		public void OnClicked() {
			UIMgr.Inst.FindPanel<SelectSavePanel>().ChooseWorld(_worldHash);
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