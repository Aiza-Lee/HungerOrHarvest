namespace GameLogic
{
	public interface ICommand {
		bool Check();
		void Execute();
		string CmdTitle { get; }
		string Description { get; }
		string FailReason { get; }
		ICommand Init(ICmdData data);
	}
}