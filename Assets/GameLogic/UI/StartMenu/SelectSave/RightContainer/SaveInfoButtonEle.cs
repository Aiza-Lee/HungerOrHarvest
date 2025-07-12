using System;
using NSFrame;
using TMPro;
using UnityEngine;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.Features.SaveLoadData;
using GameLogic.UI.Common.UiMgr;
using GameLogic.UI.Common.CenterPopUp;

namespace GameLogic.UI.StartMenu {
	public class SaveInfoButtonEle : MonoBehaviour, IGroupLayoutEle {
		[SerializeField] private float _eleSize;
		[SerializeField] private TextMeshProUGUI _daysText;
		[SerializeField] private TextMeshProUGUI _lastUpdateText;

		private SaveInfo _saveInfo;
		private RectTransform _rectTrans;
		private void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		public void SetSaveInfo(SaveInfo saveInfo) {
			_saveInfo = saveInfo;
			var extendSaveInfo = SaveSystem.LoadObject<ExtendSaveInfo>(saveInfo);
			var saveDay = extendSaveInfo.SaveDay;
			_daysText.text = 
				extendSaveInfo.IsAutoSave
					? $"第 {saveDay + 1} 天结束时"
					: "初始存档";
			_lastUpdateText.text = "存档时间: " + saveInfo.LastUpdateTime;
		}
		public void OnClicked() {
			// 弹出确认框
			var tipText = $"确定要加载存档: {_saveInfo.SaveName} ?";
			var popup = UIMgr.Inst.TogglePanel<CenterYesNoPanel>();
			popup.SetTipText(tipText);
			popup.OnYesChoosed += () => {
				SaveLoadDataAPI.LoadData(_saveInfo);
				UIMgr.Inst.TogglePanel<SelectSavePanel>();
			};
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
			_saveInfo = null;
			PoolSystem.PushGO(gameObject);
		}
		#endregion
	}
}