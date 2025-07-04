using System;
using System.Collections.Generic;
using System.Linq;

namespace OldGameLogic.Controller
{
	public static class CmdRegistry {
		private static readonly Dictionary<string, Func<List<string>, CommandBase>> _allCmds = new() {

			/* Game Operation */
				/* ViewOpt */
				{ "cam-free", (args) => new CameraFreeViewCmd(args) },
				{ "cam-focus-vill", (args) => new CameraFocusVillCmd(args) },
				{ "cam-focus-arch", (args) => new CameraFocusArchCmd(args) },
			{ "vill-new", (args) => new CreateVillCmd(args) },
			{ "vill-bond-arch", (args) => new VillBondArchCmd(args) },
			{ "vill-disbond-workarch", (args) => new VillDisbondWorkCmd(args) },
			{ "vill-disbond-home", (args) => new VillDisbondHomeCmd(args) },
			{ "arch-new", (args) => new CreateArchCmd(args) },

			{ "destroy-arch", (args) => new DestroyArchCmd(args) },
			{ "destroy-vill", (args) => new DestroyVillCmd(args) },

			{ "speed", (args) => new SetSpeedCmd(args) },
			{ "unlock-ol", (args) => new UnlockOLCmd(args) },
			{ "pause", (args) => new TogglePauseCmd(args) },

			/* SaveOpt */
			// { "save", (args) => new SaveGameCmd(args) },
			{ "auto-save", (args) => new AutoSaveGameCmd(args) },
			{ "load", (args) => new LoadGameCmd(args) },

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
