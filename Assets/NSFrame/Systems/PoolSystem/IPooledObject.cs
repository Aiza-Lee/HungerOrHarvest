namespace NSFrame
{
	/// <summary>
	/// Interface for objects that can be pooled in NSFrame.
	/// </summary>
	public interface IPooledObject {
		void InitAfterPop();
		void CleanBeforePush();
	}
}