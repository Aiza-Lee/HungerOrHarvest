using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic
{
	public class CmdTextPanel : MonoBehaviour {
		public TMP_InputField Text;
		private readonly List<string> _cmdHistory = new();
		private int _cmdHistoryIndex = 0;
		private string _curText = "";
		private bool _selected = false;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Slash) && !_selected) {
				Text.ActivateInputField();
				if (Text.text == "") Text.text = "/";
				Text.caretPosition = Text.text.Length;
			}
			if (!_selected) return;
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (_cmdHistoryIndex > 0) {
					if (_cmdHistoryIndex == _cmdHistory.Count) _curText = Text.text;
					_cmdHistoryIndex--;
					Text.text = _cmdHistory[_cmdHistoryIndex];
					Text.caretPosition = Text.text.Length;
				}
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if (_cmdHistoryIndex < _cmdHistory.Count) {
					_cmdHistoryIndex++;
					Text.text = _cmdHistoryIndex == _cmdHistory.Count ? _curText : _cmdHistory[_cmdHistoryIndex];
					Text.caretPosition = Text.text.Length;
				}
			}
			if (Input.GetKeyDown(KeyCode.Return)) {
				EventSystem.current.SetSelectedGameObject(null);
				if (Text.text != "") {
					_cmdHistory.Add(Text.text);
					_cmdHistoryIndex = _cmdHistory.Count;
					CmdRunner.Run(Text.text);
					Text.text = "";
				}
			}
			if (Input.GetKeyDown(KeyCode.Escape)) {
				EventSystem.current.SetSelectedGameObject(null);
			}
		}

		public void OnSelected() {
			// Debug.Log("OnSelected");
			_selected = true;
			DisableCameraControll();
		}
		public void OnDeselected() {
			// Debug.Log("OnDeselected");
			_selected = false;
			EnableCameraControll();
		}

		private void DisableCameraControll() {
			WorldCameraMgr.Inst.Controllable = false;
			WorldViewMgr.Inst.Controllable = false;
		}
		private void EnableCameraControll() {
			WorldCameraMgr.Inst.Controllable = true;
			WorldViewMgr.Inst.Controllable = true;
		}

	}
}