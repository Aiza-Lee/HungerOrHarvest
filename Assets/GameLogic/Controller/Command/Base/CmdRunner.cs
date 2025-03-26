namespace GameLogic
{
	public static class CmdRunner {
		public static bool Run(this ICommand cmd) {
			if (cmd.Check()) {
				cmd.Execute();
				#if UNITY_EDITOR
					UnityEngine.Debug.Log($"CMD:<<{cmd.CmdTitle}>>  {cmd.Description}");
				#endif
				return true;
			} else {
				#if UNITY_EDITOR
					UnityEngine.Debug.Log($"CMD Fail:{cmd.FailReason}");
				#endif
				return false;
			}
		}
	}
}