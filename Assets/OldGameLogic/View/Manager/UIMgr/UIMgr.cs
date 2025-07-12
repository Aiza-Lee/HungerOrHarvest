using System;
using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.View {
	/// <summary>
	/// <para> 所有的 *单例UI* 需要在编辑器中的这个单例处注册，这个类负责开始时触发所有类从而触发 NSFrame 的注册 </para>
	/// 同时也时统一触发 *单例UI* 面板的接口
	/// </summary>
	public class UIMgr : MonoSingleton<UIMgr> {

		[SerializeField] private List<PanelBase> _RegisteredPanels;

		private readonly Dictionary<Type, PanelBase> _panelDict = new();

		protected override void Awake() {
			base.Awake();
			foreach (var panel in _RegisteredPanels) {
				panel.gameObject.SetActive(true);
				_panelDict[panel.GetType()] = panel;
			}
		}

		private void Start() {
		}

		private void Update() {
#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftControl)) {
				TogglePanelImpl<Test.MainTestPanel>();
			}
#endif
		}
		private T TogglePanelImpl<T>() where T : PanelBase {
			var type = typeof(T);
			if (_panelDict.TryGetValue(type, out var panel)) {
				panel.Toggle();
				return panel as T;
			} else {
				var p = _RegisteredPanels.Where((panel) => panel.GetType() == type).First();
				_panelDict[type] = p;
				p.gameObject.SetActive(true);
				p.Toggle();
				return p as T;
			}
		}

		#region PublicMethods
		public T TogglePanel<T>() where T : PanelBase {
			return TogglePanelImpl<T>();
		}
		public void FindPanel<T>(out T panel) where T : PanelBase {
			panel = _panelDict[typeof(T)] as T;
		}
		public T FindPanel<T>() where T : PanelBase {
			return _panelDict[typeof(T)] as T;
		}

		#endregion
	}
}