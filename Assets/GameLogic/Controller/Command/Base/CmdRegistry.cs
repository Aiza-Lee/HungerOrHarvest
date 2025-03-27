using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public static class CmdRegistry {
		private static readonly Dictionary<string, Func<string[], CommandBase>> _allCmds = new() {

			/* Game Operation */
			{ "vill-new", (args) => new CreateVillCmd(args) },
			{ "arch-new", (args) => new CreateArchCmd(args) },
			{ "speed", (args) => new SetSpeedCmd(args) },
			{ "unlock-ol", (args) => new UnlockOLCmd(args) },
			{ "pause", (args) => new TogglePauseCmd(args) },

			/* SaveOpt */
			{ "save", (args) => new SaveGameCmd(args) },
			{ "world-new", (args) => new NewWorldCmd(args) },

			/* Administrator */
		};

		public static bool TryGetCmd(string cmd, string[] args, out CommandBase result) {
			if (!_allCmds.TryGetValue(cmd.ToLower(), out var generator)) {
				result = null;
				return false;
			}
			result = generator(args);
			return true;
		}
	}
}