using System.Collections.Generic;
using System.Text;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic
{
	public class ArchTestPanelInMain : MonoBehaviour {
		public Transform ArchInfoLayout; 
		public GameObject InfoPrefab;
		public TextMeshProUGUI CurPageText;

		private readonly List<TextMeshProUGUI> _ArchInfoTexts = new();
		private readonly List<ArchLogicBase> _ArchInfos = new(); 
		private readonly int[] _archFormat = { 20, 20, 20 };
		private const int PER_PAGE_ARCH_COUNT = 5;
		private const int PER_LINE_ID_COUNT = 3;
		private int _curPage;
		private readonly StringBuilder _sb = new(2048);


		public void NextPage() {
			if ((_curPage + 1) * PER_PAGE_ARCH_COUNT < _ArchInfos.Count) {
				++_curPage;
			}
		}
		public void PrevPage() {
			if (_curPage > 0) {
				--_curPage;
			}
		}


		private void OnArchAdded(ArchLogicBase arch) => _ArchInfos.Add(arch);


		private void Start() {
			var cnt = ArchInfoLayout.childCount;
			while (cnt-- != 0) {
				Destroy(ArchInfoLayout.GetChild(0).gameObject);
			}
			_ArchInfoTexts.Clear();
			for (int i = 0; i < PER_PAGE_ARCH_COUNT; ++i) {
				_ArchInfoTexts.Add(GameObject.Instantiate(InfoPrefab, ArchInfoLayout).GetComponent<TextMeshProUGUI>());
			}
		}
		private void OnEnable() {
			EventSystem.AddListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			_curPage = 0;
			var vills = WorldMgr.Inst.GetAllArchs;
			foreach (var v in vills) {
				_ArchInfos.Add(v);
			}
		}
		private void OnDisable() {
			EventSystem.RemoveListener<ArchLogicBase>((int)LogicEvt.ArchAdded_A, OnArchAdded);
			_ArchInfos.Clear();
		}

		private void Update() {
			UpdateArch();
		}
		private void UpdateArch() {
			CurPageText.text = $"Page: {_curPage + 1} / {(_ArchInfos.Count + PER_PAGE_ARCH_COUNT - 1) / PER_PAGE_ARCH_COUNT}";
			for (int i = _curPage * PER_PAGE_ARCH_COUNT, idx = 0; i < (_curPage + 1) * PER_PAGE_ARCH_COUNT; ++i, ++idx) {
				if (i < _ArchInfos.Count) {
					_sb.Clear();
					var arch = _ArchInfos[i];
					_sb.Append($"ID: {arch.ID}".PadRight(_archFormat[0]));
					_sb.Append($"{arch.OL}".PadRight(_archFormat[1]));
					_sb.Append($"Type: {arch.ArchType}".PadRight(_archFormat[2]));

					var cnt = 0;
					foreach (var vill in arch.InVills) {
						if (cnt == 0) {
							_sb.AppendLine().Append("    ");
							++cnt;
							if (cnt == PER_LINE_ID_COUNT) cnt = 0;
						}
						_sb.Append($"{vill.ID}".PadRight(_archFormat[0]));
					}

					_ArchInfoTexts[idx].text = _sb.AppendLine().ToString();
				} else {
					_ArchInfoTexts[idx].text = string.Empty;
				}
			}
		}

	}
}