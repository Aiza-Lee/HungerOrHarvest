using System;
using System.Collections.Generic;
using System.Linq;

namespace OldGameLogic.Controller
{
	public static class CmdRegistry {
		private static readonly Dictionary<string, Func<List<string>, CommandBase>> _allCmds = new() {

			/* Game Operation */
			// { "vill-new", (args) => new CreateVillCmd(args) },
			// { "vill-bond-arch", (args) => new VillBondArchCmd(args) },
			// { "vill-disbond-workarch", (args) => new VillDisbondWorkCmd(args) },
			// { "vill-disbond-home", (args) => new VillDisbondHomeCmd(args) },
			// { "arch-new", (args) => new CreateArchCmd(args) },

			// { "destroy-arch", (args) => new DestroyArchCmd(args) },
			// { "destroy-vill", (args) => new DestroyVillCmd(args) },

			// { "speed", (args) => new SetSpeedCmd(args) },
			// { "unlock-ol", (args) => new UnlockOLCmd(args) },
			// { "pause", (args) => new TogglePauseCmd(args) },

			/* Administrator */
			{ "multi", (args) => new MultiCmdCmd(args) },
		};

		public static bool TryGetCmd(string cmd, List<string> args, out CommandBase result) {
			if (!_allCmds.TryGetValue(cmd.ToLower(), out var generator)) {
				result = null;
				return false;
			}
			result = generator(args);
			return true;
		}
		
		// 获取所有命令列表
		public static List<string> GetAllCommands() {
			return _allCmds.Keys.ToList();
		}
		
		// 获取匹配前缀的命令列表
		public static List<string> GetMatchingCommands(string prefix) {
			prefix = prefix.ToLower();
			return _allCmds.Keys
				.Where(cmd => cmd.StartsWith(prefix))
				.ToList();
		}
	}
}
