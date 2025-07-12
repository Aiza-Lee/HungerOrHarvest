using System;
using System.Collections.Generic;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.Common.UiMgr {
	/// <summary>
	/// <para> 所有的 *单例UI* 需要在编辑器中的这个单例处注册，这个类负责开始时触发所有类从而触发 NSFrame 的注册 </para>
	/// 同时也时统一触发 *单例UI* 面板的接口
	/// </summary>
	public class UIMgr : MonoSingleton<UIMgr> {
		private Dictionary<Type, PanelBase> _panels;

		private void LazyInitializePanels() {
			_panels = new Dictionary<Type, PanelBase>();
			foreach (var panel in FindObjectsByType<PanelBase>(FindObjectsInactive.Include, FindObjectsSortMode.None)) {
				if (panel is IRegisterUiMgr) {
					panel.gameObject.SetActive(true);
					_panels[panel.GetType()] = panel;
				}
			}
		}

		void Start() {
			TogglePanel<StartMenu.MainPanel>();
		}

		public T TogglePanel<T>() where T : PanelBase {
			if (_panels == null) { LazyInitializePanels(); }
			var type = typeof(T);
			if (_panels.TryGetValue(type, out var panel)) {
				panel.Toggle();
				return panel as T;
			} else {
				Debug.LogWarning($"Panel of type {type} not found in UIMgr.");
				return null;
			}
		}

		public T FindPanel<T>() where T : PanelBase {
			if (_panels == null) { LazyInitializePanels(); }
			if (_panels.TryGetValue(typeof(T), out var panel)) {
				return panel as T;
			} else {
				Debug.LogWarning($"Panel of type {typeof(T)} not found in UIMgr.");
				return null;
			}
		}

		public PanelBase FindPanel(Type type) {
			if (_panels == null) { LazyInitializePanels(); }
			if (_panels.TryGetValue(type, out var panel)) {
				return panel;
			} else {
				Debug.LogWarning($"Panel of type {type} not found in UIMgr.");
				return null;
			}
		}
	}
}