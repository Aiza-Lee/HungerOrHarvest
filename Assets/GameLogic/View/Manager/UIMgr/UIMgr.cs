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

		[SerializeField] private List<Pair<ViewPanelType, PanelBase>> _RegisteredPanels;

		private readonly Dictionary<int, PanelBase> _panelDict = new();

		protected override void Awake() {
			base.Awake();
			foreach (var panel in _RegisteredPanels) { 
				panel.Value.gameObject.SetActive(true);
				_panelDict[(int)panel.Key] = panel.Value;
			}
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
				var p = _RegisteredPanels.Where((panel) => panel.Key == type).First().Value;
				_panelDict[(int)type] = p;
				p.Toggle();
			}
		}

		#region PublicMethods
		public void FindPanel<T>(out T panel) where T : PanelBase { 
			panel = _panelDict.Values.OfType<T>().First(); 
		}

		#endregion

		#region IPlayerControll
		public bool Controllable { get; set; } = true;
		#endregion
	}
}