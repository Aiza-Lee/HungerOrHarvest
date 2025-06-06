using System.Collections.Generic;
using System.Text;
using GameLogic.Model.Element.Vill;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.Test {
	public class VillTestPanelInMain : MonoBehaviour, IMananger {
		public Transform VillInfoLayout;
		public GameObject OneVillInfoPrefab;
		public TextMeshProUGUI CurPageText;

		private readonly List<TextMeshProUGUI> _VillInfoTexts = new();
		private readonly List<VillLogicBase> _VillInfos = new();
		private readonly int[] _villFormat = { 20, 20, 20, 20 };
		private const int PER_PAGE_VILL_COUNT = 20;
		private int _curPage;
		private readonly StringBuilder _sb = new(2048);


		public void NextPage() {
			if ((_curPage + 1) * PER_PAGE_VILL_COUNT < _VillInfos.Count) {
				++_curPage;
			}
		}
		public void PrevPage() {
			if (_curPage > 0) {
				--_curPage;
			}
		}


		private void OnVillAdded(VillLogicBase vill) => _VillInfos.Add(vill);
		private void OnVillDestroyed(VillLogicBase vill) => _VillInfos.Remove(vill);


		private void Start() {
			GameViewMgr.Inst.RegisterClearableMgr(this);

			var cnt = VillInfoLayout.childCount;
			while (cnt-- != 0) {
				Destroy(VillInfoLayout.GetChild(0).gameObject);
			}
			_VillInfoTexts.Clear();
			for (int i = 0; i < PER_PAGE_VILL_COUNT; ++i) {
				_VillInfoTexts.Add(GameObject.Instantiate(OneVillInfoPrefab, VillInfoLayout).GetComponent<TextMeshProUGUI>());
			}
		}
		private void OnEnable() {
			EventSystem.AddListener<VillLogicBase>((int) ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.AddListener<VillLogicBase>((int) ModelEvt.VillDestroyed_V_1, OnVillDestroyed, NSFrame.EventType.Model);
			_curPage = 0;
			var vills = WorldMgr.Inst.GetAllVills;
			foreach (var v in vills) {
				_VillInfos.Add(v);
			}
		}
		private void OnDisable() {
			EventSystem.RemoveListener<VillLogicBase>((int) ModelEvt.VillAdded_V_1, OnVillAdded, NSFrame.EventType.Model);
			EventSystem.RemoveListener<VillLogicBase>((int) ModelEvt.VillDestroyed_V_1, OnVillDestroyed, NSFrame.EventType.Model);
			_VillInfos.Clear();
		}

		private void Update() {
			UpdateVill();
		}
		private void UpdateVill() {
			CurPageText.text = $"Page: {_curPage + 1} / {(_VillInfos.Count + PER_PAGE_VILL_COUNT - 1) / PER_PAGE_VILL_COUNT}";
			for (int i = _curPage * PER_PAGE_VILL_COUNT, idx = 0; i < (_curPage + 1) * PER_PAGE_VILL_COUNT; ++i, ++idx) {
				if (i < _VillInfos.Count) {
					_sb.Clear();
					var v = _VillInfos[i];
					_sb.Append($"ID: {v.ID}".PadRight(_villFormat[0]));
					_sb.Append($"{v.Coord}".PadRight(_villFormat[1]));
					_sb.Append($"Name: {v.LastName + v.FirstName}".PadRight(_villFormat[2]));
					_sb.AppendLine();
					_VillInfoTexts[idx].text = _sb.ToString();
				} else {
					_VillInfoTexts[idx].text = string.Empty;
				}
			}
		}

		#region IClearMgr
		public void ClearMgr() {
			_VillInfos.Clear();
		}
		#endregion
	}
}