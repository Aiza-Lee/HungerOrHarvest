using System;
using System.Collections.Generic;

namespace GameLogic
{
	public static class CmdRegistry {
		private static readonly Dictionary<string, Func<List<string>, CommandBase>> _allCmds = new() {

			/* Game Operation */
				/* ViewOpt */
				{ "cam-free", (args) => new CameraFreeViewCmd(args) },
				{ "cam-follow-vill", (args) => new CameraFocusVillCmd(args) },
			{ "vill-new", (args) => new CreateVillCmd(args) },
			{ "vill-spare", (args) => new SetVillSpareCmd(args) },
			{ "vill-work", (args) => new SetVillWorkCmd(args) },
			{ "arch-new", (args) => new CreateArchCmd(args) },
			{ "speed", (args) => new SetSpeedCmd(args) },
			{ "unlock-ol", (args) => new UnlockOLCmd(args) },
			{ "pause", (args) => new TogglePauseCmd(args) },

			/* SaveOpt */
			{ "save", (args) => new SaveGameCmd(args) },
			{ "world-new", (args) => new NewWorldCmd(args) },

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
	}
}