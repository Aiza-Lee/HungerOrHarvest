namespace NSFrame
{
	/// <summary>
	/// Interface for objects that can be pooled in NSFrame.
	/// </summary>
	public interface IPooledObject {
		/// <summary>
		/// 在从对象池中取出对象时触发
		/// </summary>
		void InitAfterPop();
		/// <summary>
		/// 在对象回到对象池之前触发
		/// </summary>
		void CleanBeforePush();
	}
}