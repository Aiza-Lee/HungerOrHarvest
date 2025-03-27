using UnityEngine;

namespace GameLogic
{
	public abstract class CommandBase {
		public abstract int ArgCount { get; }
		public CommandBase(string[] args) {
			if (args.Length != ArgCount) {
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