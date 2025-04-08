using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace GameLogic.View
{
	/// <summary>
	/// <para> 所有的 UI 需要在编辑器中的这个单例处注册，这个类负责开始时触发所有类从而触发 NSFrame 的注册 </para>
	/// 同时也时统一触发 UI 面板的接口
	/// </summary>
	public class UIMgr : MonoSingleton<UIMgr>, IPlayerControll {

		public List<Pair<ViewPanelType, PanelBase>> Panels;
		private readonly Dictionary<int, PanelBase> _panelDict = new();

		public void AddPanel(ViewPanelType type, PanelBase panel) { _panelDict[(int)type] = panel; }

		protected override void Awake() {
			base.Awake();
			foreach (var panel in Panels) { panel.Value.gameObject.SetActive(true); }
		}

		private void Update() {
			if (Controllable) {
				if (Input.GetKeyDown(KeyCode.Tab)) {
					TogglePanel(ViewPanelType.MainTest);
				}
				if (Input.GetKeyDown(KeyCode.Escape)) { TogglePanel(ViewPanelType.WorldVillOperationPanel); }
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

		#region IPlayerControll
		public bool Controllable { get; set; } = true;
		#endregion
	}
}