namespace GameLogic
{
	public interface ICommand {
		bool Check();
		void Execute();
		ICommand Init(ICmdData data);
	}
}