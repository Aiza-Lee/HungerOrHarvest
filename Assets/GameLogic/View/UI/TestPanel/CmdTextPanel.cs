using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameLogic
{
	public class CmdTextPanel : MonoBehaviour {
		public TMP_InputField Text;
		private readonly List<string> _cmdHistory = new();
		private int _cmdHistoryIndex = 0;
		private string _curText = "";

		private void Update() {
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (_cmdHistoryIndex > 0) {
					if (_cmdHistoryIndex == _cmdHistory.Count) _curText = Text.text;
					_cmdHistoryIndex--;
					Text.text = _cmdHistory[_cmdHistoryIndex];
				}
			}
			if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if (_cmdHistoryIndex < _cmdHistory.Count) {
					_cmdHistoryIndex++;
					Text.text = _cmdHistoryIndex == _cmdHistory.Count ? _curText : _cmdHistory[_cmdHistoryIndex];
				}
			}
			if (Input.GetKeyDown(KeyCode.Return)) {
				if (Text.text != "") {
					_cmdHistory.Add(Text.text);
					_cmdHistoryIndex = _cmdHistory.Count;
					CmdRunner.Run(Text.text);
					Text.text = "";
				}
			}
		}

		public void OnSelected() {
			DisableCameraControll();
		}
		public void OnDeselected() {
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