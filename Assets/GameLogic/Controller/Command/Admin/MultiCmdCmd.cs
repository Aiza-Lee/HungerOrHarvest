using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public class MultiCmdCmd : CommandBase {
		private readonly int _times;
		private readonly string _insideCmd;

		public MultiCmdCmd(List<string> args) : base(args) {
			if (!ParamConverter.TryDefaultConvert(args[0], out _times)) { 
				Debug.Log($"<<{CmdTitle}>> 参数Times错误: 无法解析参数{args[0]}"); 
			}
			_insideCmd = args[1];
		}

		public override int ArgCount => 2;

		public override string CmdTitle => "多重命令";
		public override string Description => "";
		public override string FailReason => "";

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			for (int i = 0; i < _times; ++i) {
				CmdRunner.Run(_insideCmd);
			}
		}
	}
}