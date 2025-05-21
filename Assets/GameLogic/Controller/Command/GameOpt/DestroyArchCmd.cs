using System.Collections.Generic;
using GameLogic.Model.Element.Arch;
using UnityEngine;

namespace GameLogic.Controller
{
	/// <summary>
	/// Args: ArchType, OL
	/// </summary>
	public class DestroyArchCmd : CommandBase {
		private readonly ArchLogicBase _arch;
		private string _failReason;

		public DestroyArchCmd(List<string> args) : base(args) {
			if (args.Count != ArgCount) { return; }
			if (!ParamConverter.TryDefaultConvert<ulong>(args[0], out var aID)) {
				Debug.Log($"<<{CmdTitle}>> 参数ArchID错误: 无法解析参数{args[0]}");
			} else {
				_arch = WorldMgr.Inst.FindArch(aID);
			}
		}

		public override string CmdTitle => "拆除建筑";
		public override string Description => $"拆除建筑 建筑类型:{_arch.ArchType} 建筑ID:{_arch.ID}";
		public override string FailReason => _failReason;
		public override int ArgCount => 1;


		public override bool Check() {
			if (_arch == null) {
				_failReason = "建筑不存在";
				return false;
			}
			return true;
		}

		public override void Execute() {
			_arch.Destroy();
		}
	}
}