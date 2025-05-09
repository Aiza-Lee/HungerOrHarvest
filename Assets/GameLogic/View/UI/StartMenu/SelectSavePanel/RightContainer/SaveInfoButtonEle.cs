using System;
using GameLogic.Controller;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.StartMenu.SelectSavePanel
{
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
			_daysText.text = $"第 {SaveSystem.LoadObject<LogicTimeMgrSave>(saveInfo).Days + 1} 天结束时";
			_lastUpdateText.text = "存档时间: " + saveInfo.LastUpdateTime;
		}
		public void OnClicked() {
			// 弹出确认框
			var tipText = $"确定要加载存档: {_saveInfo.SaveName} ?";
			var popup = UIMgr.Inst.TogglePanel<PopUpPanels.CenterYesNoPanel>();
			popup.SetTipText(tipText);
			popup.OnYesChoosed += () => {
				GameModelMgr.Inst.SetSaveInfo(_saveInfo);
				GameViewMgr.Inst.SetSaveInfo(_saveInfo);
				UIMgr.Inst.TogglePanel<SelectSavePanel>();
				UIMgr.Inst.TogglePanel<MainPanel>();
				CmdRunner.Run("/load");
			};
		}

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set;}
		public float EleSize => _eleSize;
		public RectTransform RectTrans => _rectTrans;
		public event Action OnDirty;
		public void SetPos(float y) {
			_rectTrans.offsetMax = new(0, -y);
			_rectTrans.offsetMin = new(0, -y - _eleSize);
		}
		public void OnAddedToGroup() {
			_rectTrans.offsetMax = new(0, 0);
			_rectTrans.offsetMin = new(0, 0);
		}
		public void Clear() {
			_saveInfo = null;
			PoolSystem.PushGO(gameObject);
		}
		#endregion
	}
}