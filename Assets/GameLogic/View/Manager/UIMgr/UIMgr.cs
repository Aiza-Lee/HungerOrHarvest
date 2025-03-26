using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public class UIMgr : MonoSingleton<UIMgr> {
		public List<Pair<ViewPanelType, PanelBase>> Panels;
		private readonly Dictionary<int, PanelBase> _panelDict = new();

		public void AddPanel(ViewPanelType type, PanelBase panel) { _panelDict[(int)type] = panel; }

		protected override void Awake() {
			base.Awake();
			foreach (var panel in Panels) { panel.Value.gameObject.SetActive(true); }
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				TogglePanel(ViewPanelType.MainTest);
			}
		}

		private void TogglePanel(ViewPanelType type) {
			if (_panelDict.TryGetValue((int)type, out var panel)) {
				panel.Toggle();
			} else {
				var p = Panels.Where((panel) => panel.Key == type).First().Value;
				_panelDict[(int)type] = p;
				p.Toggle();
			}
		}
	}
}