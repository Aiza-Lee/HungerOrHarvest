namespace GameLogic
{
	public static class CmdParser {
		public static bool TryParse(string input, out string cmd, out string[] args) {
			if (!input.StartsWith("/")) {
				cmd = null; 
				args = null;
				return false;
			}
			var argStrs = input[1..].Split(' ');
			cmd = argStrs[0];
			args = argStrs[1..];
			return true;
		}
	}
}