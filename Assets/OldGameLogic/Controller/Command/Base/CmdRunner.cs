using UnityEngine;

namespace OldGameLogic.Controller
{
	public static class CmdRunner {
		public static bool Run(string cmdLine) {
			cmdLine = cmdLine.Replace("\u200B", "");
			cmdLine.Trim();
			if (CmdParser.TryParse(cmdLine, out var cmd, out var args)) {
				if (CmdRegistry.TryGetCmd(cmd, args, out var command)) {
					return command.Run();
				} else {
					Debug.LogWarning($"Cmd Not Found:{cmd}");
				}
			} else {
				Debug.LogWarning($"Cmd Parse Fail:{cmdLine}");
			}
			return false;
		}
		public static bool Run(this CommandBase cmd) {
			if (cmd.Check()) {
				cmd.Execute();
#if UNITY_EDITOR
				Debug.Log($"CMD:<<{cmd.CmdTitle}>>  {cmd.Description}");
#endif
				return true;
			} else {
#if UNITY_EDITOR
				Debug.Log($"CMD Fail:{cmd.FailReason}");
#endif
				return false;
			}
		}
	}
}