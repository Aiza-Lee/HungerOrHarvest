using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using OldGameLogic.Controller;

namespace OldGameLogic.View.Test
{
	public class CmdTextPanel : MonoBehaviour {
		public TMP_InputField Text;
		private readonly List<string> _cmdHistory = new();
		private int _cmdHistoryIndex = 0;
		/// <summary>
		/// 用户按下上箭头时保存当前指令，用于按下下箭头时恢复
		/// </summary>
		private string _curText = "";
		private bool _selected = false;
		
		// Tab补全相关字段
		private List<string> _matchingCommands = null;
		private int _completionIndex = -1;
		/// <summary>
		/// 当前用于补全的命令前缀
		/// </summary>
		private string _curPreCmd;
		/// <summary>
		/// 记录当前补全的命令是什么，用于判断用户是否在这个基础上有进行了输入
		/// </summary>
		private string _curCompletion = null;
		/// <summary>
		/// 是否处在补全模式
		/// </summary>
		private bool _tabMod;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Slash) && !_selected) {
				Text.ActivateInputField();
				ShowText(Text.text == "" ? "/" : Text.text);
			}
			if (!_selected) return;


			// Tab键补全处理
			if (Input.GetKeyDown(KeyCode.Tab)) {
				if (!_tabMod) {
					_tabMod = true;
					_curPreCmd = Text.text.TrimStart('/');
					_matchingCommands = CmdRegistry.GetMatchingCommands(_curPreCmd);
					_completionIndex = -1;
				}

				if (_matchingCommands.Count > 0) {
					// 循环选择匹配命令
					_completionIndex = (_completionIndex + 1) % _matchingCommands.Count;
					var content = "/" + _matchingCommands[_completionIndex];
					ShowText(content);
					_curCompletion = content;
				} else {
					ResetCompletionState();
				}
			} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
				if (_cmdHistoryIndex > 0) {
					if (_cmdHistoryIndex == _cmdHistory.Count) _curText = Text.text;
					_cmdHistoryIndex--;
					ShowText(_cmdHistory[_cmdHistoryIndex]);
				}
			} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
				if (_cmdHistoryIndex < _cmdHistory.Count) {
					_cmdHistoryIndex++;
					ShowText(_cmdHistoryIndex == _cmdHistory.Count ? _curText : _cmdHistory[_cmdHistoryIndex]);
				}
			} else if (Input.GetKeyDown(KeyCode.Return)) {
				ResetCompletionState();
				EventSystem.current.SetSelectedGameObject(null);
				if (Text.text != "") {
					_cmdHistory.Add(Text.text);
					_cmdHistoryIndex = _cmdHistory.Count;
					CmdRunner.Run(Text.text);
					ShowText("");
				}
			} else if (Input.GetKeyDown(KeyCode.Escape)) {
				ResetCompletionState();
				EventSystem.current.SetSelectedGameObject(null);
			}

			// 如果当前在补全模式而且用户又输入了别的内容，则重置补全功能
			if (_tabMod && _curCompletion.Length != Text.text.Length) {
				ResetCompletionState();
			}
		}

		/// <summary>
		/// 视觉上显示出文本
		/// </summary>
		/// <param name="content">文本内容</param>
		private void ShowText(string content) {
			Text.text = content;
			Text.caretPosition = Text.text.Length;
		}

		// 重置补全状态
		private void ResetCompletionState() {
			_tabMod = false;
			_matchingCommands = null;
			_completionIndex = -1;
			_curCompletion = null;
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
			ResetCompletionState();
		}

		private void DisableCameraControll() {
			// WorldCameraMgr.Inst.Controllable = false;
			// WorldViewMgr.Inst.Controllable = false;
		}
		private void EnableCameraControll() {
			// WorldCameraMgr.Inst.Controllable = true;
			// WorldViewMgr.Inst.Controllable = true;
		}
	}
}
