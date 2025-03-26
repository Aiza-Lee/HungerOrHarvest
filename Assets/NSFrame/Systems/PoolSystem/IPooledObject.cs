namespace NSFrame
{
	/// <summary>
	/// Interface for objects that can be pooled in NSFrame.
	/// </summary>
	public interface IPooledObject {
		void InitForPool();

		/// <summary>
		/// <para> Called when the object is returned to the pool.</para>
		/// Especially for GC.
		/// </summary>
		void DestroyForPool();
	}
}