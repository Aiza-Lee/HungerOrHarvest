using System.Collections.Generic;
using UnityEngine;

namespace OldGameLogic.Controller
{
	/// <summary>
	/// Args: OL
	/// </summary>
	public class UnlockOLCmd : CommandBase {
		private readonly OL _ol;
		private bool _inited = false;
		private string _failReason;

		public UnlockOLCmd(List<string> args) : base(args) {
			_inited = true;
			if (args.Count != ArgCount) { _inited = false; return; }
			if (!ParamConverter.TryConvertToOL(args[0], out _ol)) {
				_inited = false;
				Debug.Log($"<<{CmdTitle}>> 参数OL错误: 无法解析参数{args[0]}");
			}
		}

		public override string CmdTitle => "解锁OL";
		public override string Description => $"{_ol}";
		public override string FailReason => _failReason;
		public override int ArgCount => 1;


		public override bool Check() {
			if (!_inited) {
				_failReason = "参数错误";
				return false;
			}
			if (WorldMgr.Inst.IsOLUnlocked(_ol)) { 
				_failReason = $"{_ol} 已解锁";
				return false; 
			}
			return true;
		}

		public override void Execute() {
			WorldMgr.Inst.UnlockOL(_ol);
		}
	}
}