using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Controller
{
	public abstract class CommandBase {
		public abstract int ArgCount { get; }
		public CommandBase(List<string> args) {
			if (args.Count != ArgCount) {
				Debug.Log($"<<{CmdTitle}>> 参数数量错误");
				return;
			}
		}
		public abstract bool Check();
		public abstract void Execute();
		public abstract string CmdTitle { get; }
		public abstract string Description { get; }
		public abstract string FailReason { get; }
	}
}