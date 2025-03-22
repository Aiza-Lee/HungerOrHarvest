namespace NSFrame
{
	public interface IPooledObject {
		void InitForPool();
		void DestroyForPool();
	}
}