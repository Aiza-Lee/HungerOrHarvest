using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
	public static class CmdParser {
		public static bool TryParse(string input, out string cmd, out List<string> args) {
			input.TrimEnd();
			if (!input.StartsWith("/")) {
				cmd = null; 
				args = null;
				return false;
			}

			string insideCmd = null;
			int L = -1;
			for (int i = 0; i < input.Length; i++) {
				if (input[i] == '\"') {
					L = i;
					break;
				}
			}
			if (L != -1 && input[^1] == '\"') {
				insideCmd = input[(L + 1)..^1];
				input = input[0..(L-1)];
			}



			var argStrs = input[1..].Split(' ');
			cmd = argStrs[0];
			args = argStrs[1..].ToList();
			if (insideCmd != null) { args.Add(insideCmd); }
			return true;
		}
	}
}